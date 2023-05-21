using Microsoft.AspNetCore.Http;
using Proj3.Application.Common.Errors.Authentication;
using Proj3.Application.Common.Interfaces.Others;
using Proj3.Application.Common.Interfaces.Persistence.Authentication;
using Proj3.Application.Common.Interfaces.Services.Authentication.Command;
using Proj3.Application.Common.Interfaces.Utils.Authentication;
using Proj3.Application.Services.Authentication.Result;
using Proj3.Application.Utils.Authentication;
using Proj3.Domain.Entities.Authentication;
using System.Security.Claims;

namespace Proj3.Application.Services.Authentication.Commands;

public class AuthenticationCommandService : IAuthenticationCommandService
{
    private readonly ITokensUtils _tokensUtils;
    private readonly IEmailUtils _emailUtils;
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokensRepository;
    private readonly IUserValidationCodeRepository _userValidationCodeRepository;
    private readonly ITransactionsManager _transactionsManager;

    public AuthenticationCommandService(ITokensUtils tokensUtils, IEmailUtils emailUtils, IUserRepository userRepository, IRefreshTokenRepository refreshTokensRepository, IUserValidationCodeRepository userValidationCodeRepository, ITransactionsManager transactionsManager)
    {
        _tokensUtils = tokensUtils;
        _emailUtils = emailUtils;
        _userRepository = userRepository;
        _refreshTokensRepository = refreshTokensRepository;
        _userValidationCodeRepository = userValidationCodeRepository;
        _transactionsManager = transactionsManager;
    }

    public async Task<UserStatusResult> SignUpNgo(string name, string email, string password)
    {        
        if (await _userRepository.GetUserByEmail(email) is Domain.Entities.Authentication.User userCheck && userCheck.Active)
        {
            throw new UserAlreadyExistsException();
        }
        
        if (!Validation.IsValidEmail(email))
        {
            throw new InvalidEmailException();
        }
        
        if (!Validation.IsValidPassword(password))
        {
            throw new InvalidPasswordException();
        }

        Domain.Entities.Authentication.User? user = Domain.Entities.Authentication.User.NewUserNgo(
            name: name,
            email: email
        );      
        user.Salt = Crypto.GetSalt;
        user.PasswordHash = Crypto.ReturnUserHash(user, password);

        await _transactionsManager.BeginTransactionAsync();

        await _userRepository.Add(user);                   
        UserValidationCode uvEmail = new (user.Id, user.Email);
        await _emailUtils.SendEmail(user.Email, "Código de Confirmação de email", $"Código: {uvEmail.Code}");
        await _userValidationCodeRepository.Add(uvEmail);

        await _transactionsManager.CommitTransactionAsync();

        return new UserStatusResult(user);
    }

    public async Task<UserStatusResult> SignUpVolunteer(string name, string email, string password)
    {        
        if (await _userRepository.GetUserByEmail(email) is Domain.Entities.Authentication.User userCheck && userCheck.Active)
        {
            throw new UserAlreadyExistsException();
        }

        if (!Validation.IsValidEmail(email))
        {
            throw new InvalidEmailException();
        }

        if (!Validation.IsValidPassword(password))
        {
            throw new InvalidPasswordException();
        }

        Domain.Entities.Authentication.User? user = Domain.Entities.Authentication.User.NewUserVolunteer(
            name: name,
            email: email            
        );
        user.Salt = Crypto.GetSalt;
        user.PasswordHash = Crypto.ReturnUserHash(user, password);

        await _transactionsManager.BeginTransactionAsync();

        var addedUser = await _userRepository.Add(user);
        UserValidationCode uvEmail = new (user.Id, user.Email);
        await _userValidationCodeRepository.Add(uvEmail);

        await _transactionsManager.CommitTransactionAsync();

        return new UserStatusResult(user);
    }

    public AuthenticationResult RefreshToken(string refreshtoken, string acesstoken)
    {
        if (_tokensUtils.ValidateJwtToken(acesstoken) is null)
        {
            throw new InvalidAcessTokenException();
        }

        if (_refreshTokensRepository.GetByToken(refreshtoken).Result is not RefreshToken rf)
        {
            throw new InvalidRefreshTokenException();
        }

        Domain.Entities.Authentication.User user = _userRepository.GetUserById(rf.UserId).Result!;
        ClaimsPrincipal claimsPrincipal = _tokensUtils.ExtractClaimsFromToken(acesstoken);

        string newAccessToken = _tokensUtils.GenerateJwtToken(user, claimsPrincipal);
        RefreshToken newRefreshToken = _tokensUtils.GenerateRefreshToken(user, claimsPrincipal);

        _refreshTokensRepository.Update(newRefreshToken);

        return new AuthenticationResult(
            user,
            newAccessToken,
            newRefreshToken.Token
        );
    }

    public async Task<bool> Logout(HttpContext httpContext)
    {
        Guid userId = Utils.Authentication.User.GetUserIdFromHttpContext(httpContext);

        await _refreshTokensRepository.RevokeAllTokensFromUser(userId);
        return true;        
    }

    public async Task<AuthenticationResult> ChangePassword(string email, string oldPassword, string newPassword)
    {
        if (!(await _userRepository.GetUserByEmail(email) is Domain.Entities.Authentication.User user && user.PasswordHash == Crypto.ReturnUserHash(user, oldPassword)))
        {
            throw new InvalidCredentialsException();
        }        

        if (!Validation.IsValidPassword(newPassword))
        {
            throw new InvalidPasswordException();
        }

        await _transactionsManager.BeginTransactionAsync();

        user.PasswordHash = Crypto.ReturnUserHash(user, newPassword);
        string? acessToken = _tokensUtils.GenerateJwtToken(user);
        RefreshToken? refreshToken = _tokensUtils.GenerateRefreshToken(user);
        await _userRepository.Update(user);

        await _transactionsManager.CommitTransactionAsync();

        return new AuthenticationResult(
            user,
            acessToken,
            refreshToken.Token
        );
    }

    public async Task<UserStatusResult> ConfirmEmail(Guid userId, int code)
    {
        if (await _userValidationCodeRepository.GetEmailValidationCodeByUser((await _userRepository.GetUserById(userId))!) is not UserValidationCode uv)
        {
            throw new Exception("This confirmation not exists.");
        }

        if (uv.Expiration < DateTime.Now)
        {
            throw new Exception("Confirmation code has expired.");
        }

        if (uv.Code != code)
        {
            throw new Exception("Invalid confirmation code.");
        }

        await _transactionsManager.BeginTransactionAsync();

        await _userValidationCodeRepository.RemoveUserConfirmation(uv);
        Domain.Entities.Authentication.User? user = await _userRepository.GetUserById(userId);
        user!.Active = true;
        await _userRepository.Update(user);

        await _transactionsManager.CommitTransactionAsync();

        return new UserStatusResult(user);        
    }
}
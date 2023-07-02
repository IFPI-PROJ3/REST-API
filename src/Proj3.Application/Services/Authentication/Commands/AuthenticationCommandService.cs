using Microsoft.AspNetCore.Http;
using Proj3.Application.Common.Errors.Authentication;
using Proj3.Application.Common.Interfaces.Others;
using Proj3.Application.Common.Interfaces.Persistence.Authentication;
using Proj3.Application.Common.Interfaces.Persistence.Common;
using Proj3.Application.Common.Interfaces.Persistence.NGO;
using Proj3.Application.Common.Interfaces.Persistence.Volunteer;
using Proj3.Application.Common.Interfaces.Services.Authentication.Command;
using Proj3.Application.Common.Interfaces.Utils.Authentication;
using Proj3.Application.Services.Authentication.Result;
using Proj3.Application.Utils.Authentication;
using Proj3.Contracts.Authentication.Request;
using Proj3.Domain.Entities.Authentication;
using Proj3.Domain.Entities.NGO;
using System.Security.Claims;

namespace Proj3.Application.Services.Authentication.Commands;

public class AuthenticationCommandService : IAuthenticationCommandService
{
    private readonly ITokensUtils _tokensUtils;
    private readonly IEmailUtils _emailUtils;        
    private readonly IUserRepository _userRepository;
    private readonly INgoRepository _ngoRepository;
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IRefreshTokenRepository _refreshTokensRepository;
    private readonly IUserValidationCodeRepository _userValidationCodeRepository;
    private readonly ITransactionsManager _transactionsManager;

    public AuthenticationCommandService(ITokensUtils tokensUtils, 
        IEmailUtils emailUtils,
        INgoRepository ngoRepository,
        IVolunteerRepository volunteerRepository,
        ICategoryRepository categoryRepository,
        IUserRepository userRepository, 
        IRefreshTokenRepository refreshTokensRepository, 
        IUserValidationCodeRepository userValidationCodeRepository, 
        ITransactionsManager transactionsManager)
    {
        _tokensUtils = tokensUtils;
        _emailUtils = emailUtils;
        _ngoRepository = ngoRepository;
        _volunteerRepository = volunteerRepository;
        _categoryRepository = categoryRepository;
        _userRepository = userRepository;
        _refreshTokensRepository = refreshTokensRepository;
        _userValidationCodeRepository = userValidationCodeRepository;
        _transactionsManager = transactionsManager;
    }

    public async Task<UserStatusResult> SignUpNgoAsync(SignUpNgoRequest signUpNgoRequest)
    {
        if (await _userRepository.GetUserByEmailAsync(signUpNgoRequest.email) is Domain.Entities.Authentication.User userCheck && userCheck.Active)
        {
            throw new UserAlreadyExistsException();
        }
        
        if (!Validation.IsValidEmail(signUpNgoRequest.email))
        {
            throw new InvalidEmailException();
        }
        
        if (!Validation.IsValidPassword(signUpNgoRequest.password))
        {
            throw new InvalidPasswordException();
        }

        Domain.Entities.Authentication.User? user = Domain.Entities.Authentication.User.NewUserNgo(
            name: signUpNgoRequest.username,
            email: signUpNgoRequest.email
        );        
        user.Salt = Crypto.GetSalt;
        user.PasswordHash = Crypto.ReturnUserHash(user, signUpNgoRequest.password);

        Ngo ngo = new(user.Id, signUpNgoRequest.name, signUpNgoRequest.description, signUpNgoRequest.latitude, signUpNgoRequest.longitude);

        try
        {
            await _transactionsManager.BeginTransactionAsync();

            await _userRepository.AddAsync(user);
            await _ngoRepository.AddAsync(ngo);

            await _categoryRepository.AddCategoriesToNgoAsync(ngo.Id, signUpNgoRequest.categories);

            UserValidationCode uvEmail = new(user.Id, user.Email);
            await _emailUtils.SendEmail(user.Email, "C�digo de Confirma��o de email", $"C�digo: {uvEmail.Code}");
            await _userValidationCodeRepository.AddAsync(uvEmail);

            await _transactionsManager.CommitTransactionAsync();

            return new UserStatusResult(user);
        } 
        catch (Exception)
        {
            await _transactionsManager.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task<UserStatusResult> SignUpVolunteerAsync(SignUpVolunteerRequest signUpVolunteerRequest)
    {
        if (await _userRepository.GetUserByEmailAsync(signUpVolunteerRequest.email) is Domain.Entities.Authentication.User userCheck && userCheck.Active)
        {
            throw new UserAlreadyExistsException();
        }

        if (!Validation.IsValidEmail(signUpVolunteerRequest.email))
        {
            throw new InvalidEmailException();
        }

        if (!Validation.IsValidPassword(signUpVolunteerRequest.password))
        {
            throw new InvalidPasswordException();
        }

        Domain.Entities.Authentication.User? user = Domain.Entities.Authentication.User.NewUserVolunteer(
            name: signUpVolunteerRequest.username,
            email: signUpVolunteerRequest.email
        );
        
        user.Salt = Crypto.GetSalt;
        user.PasswordHash = Crypto.ReturnUserHash(user, signUpVolunteerRequest.password);

        Domain.Entities.Volunteer.Volunteer volunteer = new(
            user.Id, 
            signUpVolunteerRequest.name, 
            signUpVolunteerRequest.lastname, 
            signUpVolunteerRequest.description, 
            signUpVolunteerRequest.latitude, 
            signUpVolunteerRequest.longitude
        );

        try
        {
            await _transactionsManager.BeginTransactionAsync();

            await _userRepository.AddAsync(user);
            await _categoryRepository.AddCategoriesToVolunteerAsync(volunteer.Id, signUpVolunteerRequest.categories);

            UserValidationCode uvEmail = new(user.Id, user.Email);
            await _userValidationCodeRepository.AddAsync(uvEmail);

            await _transactionsManager.CommitTransactionAsync();

            return new UserStatusResult(user);
        }
        catch (Exception)
        {
            await _transactionsManager.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task<AuthenticationResult> RefreshTokenAsync(RefreshTokenRequest refreshTokenRequest)
    {
        if (_tokensUtils.ValidateJwtToken(refreshTokenRequest.access_token) is null)
        {
            throw new InvalidAcessTokenException();
        }

        if (await _refreshTokensRepository.GetByTokenAsync(refreshTokenRequest.refresh_token) is not RefreshToken rf)
        {
            throw new InvalidRefreshTokenException();
        }

        Domain.Entities.Authentication.User user = (await _userRepository.GetUserByIdAsync(rf.UserId))!;
        ClaimsPrincipal claimsPrincipal = _tokensUtils.ExtractClaimsFromToken(refreshTokenRequest.access_token);

        string newAccessToken = _tokensUtils.GenerateJwtToken(user, claimsPrincipal);
        RefreshToken newRefreshToken = _tokensUtils.GenerateRefreshToken(user, claimsPrincipal);

        await _refreshTokensRepository.AddAsync(newRefreshToken);

        return new AuthenticationResult(
            user,
            newAccessToken,
            newRefreshToken.Token
        );
    }

    public async Task<bool> LogoutAsync(HttpContext httpContext)
    {
        Guid userId = Utils.Authentication.User.GetUserIdFromHttpContext(httpContext);

        await _refreshTokensRepository.RevokeAllTokensFromUserAsync(userId);
        return true;        
    }

    public async Task<AuthenticationResult> ChangePasswordAsync(ChangePasswordRequest changePasswordRequest)
    {
        if (!(await _userRepository.GetUserByEmailAsync(changePasswordRequest.email) is Domain.Entities.Authentication.User user && user.PasswordHash == Crypto.ReturnUserHash(user, changePasswordRequest.old_password)))
        {
            throw new InvalidCredentialsException();
        }        

        if (!Validation.IsValidPassword(changePasswordRequest.new_password))
        {
            throw new InvalidPasswordException();
        }

        await _transactionsManager.BeginTransactionAsync();

        user.PasswordHash = Crypto.ReturnUserHash(user, changePasswordRequest.new_password);
        string? acessToken = _tokensUtils.GenerateJwtToken(user);
        RefreshToken? refreshToken = _tokensUtils.GenerateRefreshToken(user);
        await _userRepository.UpdateAsync(user);

        await _transactionsManager.CommitTransactionAsync();

        return new AuthenticationResult(
            user,
            acessToken,
            refreshToken.Token
        );
    }

    public async Task<UserStatusResult> ConfirmEmailAsync(ConfirmationRequest confirmationRequest)
    {
        if (await _userValidationCodeRepository.GetEmailValidationCodeByUserAsync((await _userRepository.GetUserByIdAsync(confirmationRequest.user_id))!) is not UserValidationCode uv)
        {
            throw new Exception("This confirmation not exists.");
        }

        if (uv.Expiration < DateTime.Now)
        {
            throw new Exception("Confirmation code has expired.");
        }

        if (uv.Code != confirmationRequest.code)
        {
            throw new Exception("Invalid confirmation code.");
        }

        await _transactionsManager.BeginTransactionAsync();

        await _userValidationCodeRepository.RemoveUserConfirmationAsync(uv);
        Domain.Entities.Authentication.User? user = await _userRepository.GetUserByIdAsync(confirmationRequest.user_id);
        user!.Active = true;
        await _userRepository.UpdateAsync(user);

        await _transactionsManager.CommitTransactionAsync();

        return new UserStatusResult(user);        
    }
}
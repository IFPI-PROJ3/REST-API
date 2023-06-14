using Microsoft.AspNetCore.Http;
using Proj3.Application.Common.Errors.Authentication;
using Proj3.Application.Common.Interfaces.Others;
using Proj3.Application.Common.Interfaces.Persistence.Authentication;
using Proj3.Application.Common.Interfaces.Persistence.Common;
using Proj3.Application.Common.Interfaces.Persistence.NGO;
using Proj3.Application.Common.Interfaces.Services.Authentication.Command;
using Proj3.Application.Common.Interfaces.Utils.Authentication;
using Proj3.Application.Services.Authentication.Result;
using Proj3.Application.Utils.Authentication;
using Proj3.Contracts.Authentication.Request;
using Proj3.Domain.Entities.Authentication;
using Proj3.Domain.Entities.NGO;
using Proj3.Domain.Entities.Volunteer;
using System.Security.Claims;

namespace Proj3.Application.Services.Authentication.Commands;

public class AuthenticationCommandService : IAuthenticationCommandService
{
    private readonly ITokensUtils _tokensUtils;
    private readonly IEmailUtils _emailUtils;        
    private readonly IUserRepository _userRepository;
    private readonly INgoRepository _ngoRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IRefreshTokenRepository _refreshTokensRepository;
    private readonly IUserValidationCodeRepository _userValidationCodeRepository;
    private readonly ITransactionsManager _transactionsManager;

    public AuthenticationCommandService(ITokensUtils tokensUtils, 
        IEmailUtils emailUtils,
        INgoRepository ngoRepository,
        ICategoryRepository categoryRepository,
        IUserRepository userRepository, 
        IRefreshTokenRepository refreshTokensRepository, 
        IUserValidationCodeRepository userValidationCodeRepository, 
        ITransactionsManager transactionsManager)
    {
        _tokensUtils = tokensUtils;
        _emailUtils = emailUtils;
        _ngoRepository = ngoRepository;
        _categoryRepository = categoryRepository;
        _userRepository = userRepository;
        _refreshTokensRepository = refreshTokensRepository;
        _userValidationCodeRepository = userValidationCodeRepository;
        _transactionsManager = transactionsManager;
    }

    public async Task<UserStatusResult> SignUpNgo(SignUpNgoRequest signUpNgoRequest)
    {
        // VALIDATING REQUEST
        if (await _userRepository.GetUserByEmail(signUpNgoRequest.email) is Domain.Entities.Authentication.User userCheck && userCheck.Active)
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

        // CREATING ENTITIES
        Domain.Entities.Authentication.User? user = Domain.Entities.Authentication.User.NewUserNgo(
            name: signUpNgoRequest.username,
            email: signUpNgoRequest.email
        );        
        user.Salt = Crypto.GetSalt;
        user.PasswordHash = Crypto.ReturnUserHash(user, signUpNgoRequest.password);

        Ngo ngo = new(user.Id, signUpNgoRequest.name, signUpNgoRequest.password, signUpNgoRequest.latitude, signUpNgoRequest.longitude);

        try
        {
            // TRANSACTION ADD USER AND NGO
            await _transactionsManager.BeginTransactionAsync();

            await _userRepository.Add(user);
            await _ngoRepository.Add(ngo);

            await _categoryRepository.AddCategoriesToNgoAsync(ngo.Id, signUpNgoRequest.categories);

            UserValidationCode uvEmail = new(user.Id, user.Email);
            await _emailUtils.SendEmail(user.Email, "Código de Confirmação de email", $"Código: {uvEmail.Code}");
            await _userValidationCodeRepository.Add(uvEmail);

            await _transactionsManager.CommitTransactionAsync();

            return new UserStatusResult(user);
        } 
        catch (Exception)
        {
            await _transactionsManager.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task<UserStatusResult> SignUpVolunteer(SignUpVolunteerRequest signUpVolunteerRequest)
    {
        // VALIDATING REQUEST
        if (await _userRepository.GetUserByEmail(signUpVolunteerRequest.email) is Domain.Entities.Authentication.User userCheck && userCheck.Active)
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

        // CREATING ENTITIES
        Domain.Entities.Authentication.User? user = Domain.Entities.Authentication.User.NewUserVolunteer(
            name: signUpVolunteerRequest.username,
            email: signUpVolunteerRequest.email
        );
        user.Salt = Crypto.GetSalt;
        user.PasswordHash = Crypto.ReturnUserHash(user, signUpVolunteerRequest.password);

        Volunteer volunteer = new(user.Id, signUpVolunteerRequest.name, signUpVolunteerRequest.lastname, signUpVolunteerRequest.description);

        try
        {
            // TRANSACTION ADD USER AND VOLUNTEER
            await _transactionsManager.BeginTransactionAsync();

            await _userRepository.Add(user);
            await _categoryRepository.AddCategoriesToVolunteerAsync(volunteer.Id, signUpVolunteerRequest.categories);

            UserValidationCode uvEmail = new(user.Id, user.Email);
            await _userValidationCodeRepository.Add(uvEmail);

            await _transactionsManager.CommitTransactionAsync();

            return new UserStatusResult(user);
        }
        catch (Exception)
        {
            await _transactionsManager.RollbackTransactionAsync();
            throw;
        }
    }

    public AuthenticationResult RefreshToken(RefreshTokenRequest refreshTokenRequest)
    {
        // VALIDATING REQUEST
        if (_tokensUtils.ValidateJwtToken(refreshTokenRequest.access_token) is null)
        {
            throw new InvalidAcessTokenException();
        }

        if (_refreshTokensRepository.GetByToken(refreshTokenRequest.refresh_token).Result is not RefreshToken rf)
        {
            throw new InvalidRefreshTokenException();
        }

        Domain.Entities.Authentication.User user = _userRepository.GetUserById(rf.UserId).Result!;
        ClaimsPrincipal claimsPrincipal = _tokensUtils.ExtractClaimsFromToken(refreshTokenRequest.access_token);

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

    public async Task<AuthenticationResult> ChangePassword(ChangePasswordRequest changePasswordRequest)
    {
        if (!(await _userRepository.GetUserByEmail(changePasswordRequest.email) is Domain.Entities.Authentication.User user && user.PasswordHash == Crypto.ReturnUserHash(user, changePasswordRequest.old_password)))
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
        await _userRepository.Update(user);

        await _transactionsManager.CommitTransactionAsync();

        return new AuthenticationResult(
            user,
            acessToken,
            refreshToken.Token
        );
    }

    public async Task<UserStatusResult> ConfirmEmail(ConfirmationRequest confirmationRequest)
    {
        if (await _userValidationCodeRepository.GetEmailValidationCodeByUser((await _userRepository.GetUserById(confirmationRequest.user_id))!) is not UserValidationCode uv)
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

        await _userValidationCodeRepository.RemoveUserConfirmation(uv);
        Domain.Entities.Authentication.User? user = await _userRepository.GetUserById(confirmationRequest.user_id);
        user!.Active = true;
        await _userRepository.Update(user);

        await _transactionsManager.CommitTransactionAsync();

        return new UserStatusResult(user);        
    }
}
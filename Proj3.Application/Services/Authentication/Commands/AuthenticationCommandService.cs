using Proj3.Application.Common.Errors.Authentication;
using Proj3.Application.Common.Interfaces.Persistence.Authentication;
using Proj3.Application.Common.Interfaces.Services.Authentication.Command;
using Proj3.Application.Common.Interfaces.Utils.Authentication;
using Proj3.Application.Services.Authentication.Result;
using Proj3.Application.Utils.Authentication;
using Proj3.Domain.Entities.Authentication;

namespace Proj3.Application.Services.Authentication.Commands;

public class AuthenticationCommandService : IAuthenticationCommandService
{
    private readonly ITokensUtils _tokensUtils;
    private readonly IEmailUtils _emailUtils;
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokensRepository;
    private readonly IUserValidationCodeRepository _userValidationCodeRepository;

    public AuthenticationCommandService(ITokensUtils tokensUtils, IEmailUtils emailUtils, IUserRepository userRepository, IRefreshTokenRepository refreshTokensRepository, IUserValidationCodeRepository userValidationCodeRepository)
    {
        _tokensUtils = tokensUtils;
        _emailUtils = emailUtils;
        _userRepository = userRepository;
        _refreshTokensRepository = refreshTokensRepository;
        _userValidationCodeRepository = userValidationCodeRepository;
    }

    public async Task<UserStatusResult> SignUpNgo(string name, string email, string password)
    {        
        if (await _userRepository.GetUserByEmail(email) is User userCheck && userCheck.Active)
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
        
        User? user = User.NewUserNgo(
            name: name,
            email: email
        );
      
        user.Salt = Crypto.GetSalt;
        user.PasswordHash = Crypto.ReturnUserHash(user, password);        
        
        await _userRepository.Add(user);
                   
        UserValidationCode uvEmail = new UserValidationCode(user.Id, user.Email);

        await _emailUtils.SendEmail(user.Email, "Código de Confirmação de email", $"Código: {uvEmail.Code}");
        await _userValidationCodeRepository.Add(uvEmail);

        return new UserStatusResult(user);
    }

    public async Task<UserStatusResult> SignUpVolunteer(string name, string email, string password)
    {
        if (await _userRepository.GetUserByEmail(email) is User userCheck && userCheck.Active)
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
        
        User? user = User.NewUserVolunteer(
            name: name,
            email: email            
        );

        user.Salt = Crypto.GetSalt;
        user.PasswordHash = Crypto.ReturnUserHash(user, password);

        await _userRepository.Add(user);

        UserValidationCode uvEmail = new UserValidationCode(user.Id, user.Email);
        await _userValidationCodeRepository.Add(uvEmail);

        return new UserStatusResult(user);
    }

    public async Task<AuthenticationResult> ChangePassword(string email, string oldPassword, string newPassword)
    {
        if (!(await _userRepository.GetUserByEmail(email) is User user && user.PasswordHash == Crypto.ReturnUserHash(user, oldPassword)))
        {
            throw new InvalidCredentialsException();
        }        

        if (!Validation.IsValidPassword(newPassword))
        {
            throw new InvalidPasswordException();
        }

        user.PasswordHash = Crypto.ReturnUserHash(user, newPassword);

        string? acessToken = _tokensUtils.GenerateJwtToken(user);
        RefreshToken? refreshToken = _tokensUtils.GenerateRefreshToken(user);

        return new AuthenticationResult(
            await _userRepository.Update(user),
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
        
        await _userValidationCodeRepository.RemoveUserConfirmation(uv);

        User? user = await _userRepository.GetUserById(userId);
        user!.Active = true;

        return new UserStatusResult(await _userRepository.Update(user));
    }    
}
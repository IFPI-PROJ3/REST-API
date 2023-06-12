using Microsoft.AspNetCore.Http;
using Proj3.Application.Services.Authentication.Result;
using Proj3.Contracts.Authentication.Request;

namespace Proj3.Application.Common.Interfaces.Services.Authentication.Command
{
    public interface IAuthenticationCommandService
    {
        Task<UserStatusResult> SignUpNgo(SignUpNgoRequest signUpRequest);
        Task<UserStatusResult> SignUpVolunteer(SignUpVolunteerRequest signUpRequest);        
        AuthenticationResult RefreshToken(RefreshTokenRequest refreshTokenRequest);
        Task<bool> Logout(HttpContext httpContext);
        Task<AuthenticationResult> ChangePassword(ChangePasswordRequest changePasswordRequest);        
        Task<UserStatusResult> ConfirmEmail(ConfirmationRequest confirmationRequest);
    }
}
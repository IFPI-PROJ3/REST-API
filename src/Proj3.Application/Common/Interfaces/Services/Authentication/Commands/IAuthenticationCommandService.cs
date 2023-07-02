using Microsoft.AspNetCore.Http;
using Proj3.Application.Services.Authentication.Result;
using Proj3.Contracts.Authentication.Request;

namespace Proj3.Application.Common.Interfaces.Services.Authentication.Command
{
    public interface IAuthenticationCommandService
    {
        Task<UserStatusResult> SignUpNgoAsync(SignUpNgoRequest signUpRequest);

        Task<UserStatusResult> SignUpVolunteerAsync(SignUpVolunteerRequest signUpRequest);
        
        Task<AuthenticationResult> RefreshTokenAsync(RefreshTokenRequest refreshTokenRequest);

        Task<bool> LogoutAsync(HttpContext httpContext);

        Task<AuthenticationResult> ChangePasswordAsync(ChangePasswordRequest changePasswordRequest);  
        
        Task<UserStatusResult> ConfirmEmailAsync(ConfirmationRequest confirmationRequest);
    }
}
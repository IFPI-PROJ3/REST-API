using Microsoft.AspNetCore.Http;
using Proj3.Application.Services.Authentication.Result;

namespace Proj3.Application.Common.Interfaces.Services.Authentication.Command
{
    public interface IAuthenticationCommandService
    {
        Task<UserStatusResult> SignUpVolunteer(string name, string email, string password);
        Task<UserStatusResult> SignUpNgo(string name, string email, string password);
        Task<AuthenticationResult> RefreshToken(string refreshtoken, string acesstoken);
        Task<bool> Logout(HttpContext httpContext);
        Task<AuthenticationResult> ChangePassword(string email, string oldPassword, string newPassword);        
        Task<UserStatusResult> ConfirmEmail(Guid userId, int code);
    }
}
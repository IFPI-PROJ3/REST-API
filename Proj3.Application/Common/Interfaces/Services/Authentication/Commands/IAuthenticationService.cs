using Proj3.Application.Services.Authentication.Result;

namespace Proj3.Application.Common.Interfaces.Services.Authentication.Command
{
    public interface IAuthenticationCommandService
    {
        Task<UserStatusResult> SignUpVolunteer(string name, string email, string password);
        Task<UserStatusResult> SignUpNgo(string name, string email, string password);
        Task<AuthenticationResult> ChangePassword(Guid id, string password);
        Task<UserStatusResult> ConfirmEmail(Guid userId, int code);
    }
}
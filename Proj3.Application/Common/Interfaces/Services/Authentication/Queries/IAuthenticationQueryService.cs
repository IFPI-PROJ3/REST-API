using Proj3.Application.Services.Authentication.Result;

namespace Proj3.Application.Common.Interfaces.Services.Authentication.Queries
{
    public interface IAuthenticationQueryService
    {
        Task<AuthenticationResult> SignIn(string email, string password);                           
    }
}
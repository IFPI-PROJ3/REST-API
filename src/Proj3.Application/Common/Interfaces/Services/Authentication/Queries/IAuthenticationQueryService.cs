using Proj3.Application.Services.Authentication.Result;
using Proj3.Contracts.Authentication.Request;

namespace Proj3.Application.Common.Interfaces.Services.Authentication.Queries
{
    public interface IAuthenticationQueryService
    {
        Task<AuthenticationResult> SignIn(SignInRequest signInRequest);                           
    }
}
using Microsoft.AspNetCore.Http;
using Proj3.Application.Common.Errors.Authentication;
using System.Security.Claims;

namespace Proj3.Application.Utils.Authentication
{
    public static class User
    {
        public static Guid GetUserIdFromHttpContext(HttpContext httpContext)
        {            
            if (httpContext.User.FindFirst(ClaimTypes.NameIdentifier) is not Claim claim || claim.Value == "")
            {
                throw new InvalidCredentialsException();
            }

            return new Guid(claim.Value);
        }        
    }
}

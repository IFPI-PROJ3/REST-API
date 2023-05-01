using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Proj3.Application.Utils.Authentication
{
    public static class User
    {
        public static Guid GetUserIdFromHttpContext(HttpContext httpContext)
        {
            return new Guid(httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        }        
    }
}

using System.Net;

namespace Proj3.Application.Common.Errors.Authentication
{
    public class InvalidRefreshTokenException : Exception, IExceptionBase
    {
        public HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;

        public string ErrorMessage => "Refresh token is invalid.";
    }
}
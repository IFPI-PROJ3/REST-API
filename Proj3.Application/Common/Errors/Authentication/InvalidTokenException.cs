

using System.Net;

namespace Proj3.Application.Common.Errors.Authentication
{
    public class InvalidTokenException : Exception, IExceptionBase
    {
        public HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;

        public string ErrorMessage => "Token is invalid.";
    }
}
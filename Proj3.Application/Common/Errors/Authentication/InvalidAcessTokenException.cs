using System.Net;

namespace Proj3.Application.Common.Errors.Authentication
{
    public class InvalidAcessTokenException : Exception, IExceptionBase
    {
        public HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;

        public string ErrorMessage => "Access token is invalid.";
    }
}
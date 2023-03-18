using System.Net;

namespace Proj3.Application.Common.Errors.Authentication
{
    public class InvalidCredentialsException : Exception, IExceptionBase
    {
        public HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;

        public string ErrorMessage => "Invalid credentials.";
    }
}

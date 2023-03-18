using System.Net;

namespace Proj3.Application.Common.Errors.Authentication
{
    public class UserAlreadyExistsException : Exception, IExceptionBase
    {
        public HttpStatusCode StatusCode => HttpStatusCode.Conflict;
        public string ErrorMessage => "User already exists.";
    }
}
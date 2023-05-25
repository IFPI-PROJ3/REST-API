using Microsoft.AspNetCore.Http;

namespace Proj3.Application.Common.Errors.Authentication
{
    public class UserAlreadyExistsException : Exception, IExceptionBase
    {
        public int StatusCode => StatusCodes.Status409Conflict;
        public string ErrorMessage => "User already exists.";
    }
}
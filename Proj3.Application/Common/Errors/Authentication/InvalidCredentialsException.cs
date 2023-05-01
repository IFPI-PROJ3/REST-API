using Microsoft.AspNetCore.Http;
using System.Net;

namespace Proj3.Application.Common.Errors.Authentication
{
    public class InvalidCredentialsException : Exception, IExceptionBase
    {
        public int StatusCode => StatusCodes.Status401Unauthorized;

        public string ErrorMessage => "Invalid credentials.";
    }
}

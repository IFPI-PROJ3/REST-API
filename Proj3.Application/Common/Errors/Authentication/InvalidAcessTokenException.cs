using Microsoft.AspNetCore.Http;
using System.Net;

namespace Proj3.Application.Common.Errors.Authentication
{
    public class InvalidAcessTokenException : Exception, IExceptionBase
    {
        public int StatusCode => StatusCodes.Status401Unauthorized;

        public string ErrorMessage => "Access token is invalid.";
    }
}
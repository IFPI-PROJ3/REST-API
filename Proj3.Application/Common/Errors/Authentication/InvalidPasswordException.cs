using Microsoft.AspNetCore.Http;
using System.Net;

namespace Proj3.Application.Common.Errors.Authentication;

public class InvalidPasswordException : Exception, IExceptionBase
{
    public int StatusCode => StatusCodes.Status422UnprocessableEntity;

    public string ErrorMessage => "Password is invalid.";
}
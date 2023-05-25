using Microsoft.AspNetCore.Http;

namespace Proj3.Application.Common.Errors.Authentication;

public class InvalidEmailException : Exception, IExceptionBase
{
    public int StatusCode => StatusCodes.Status422UnprocessableEntity;

    public string ErrorMessage => "This email is invalid.";
}
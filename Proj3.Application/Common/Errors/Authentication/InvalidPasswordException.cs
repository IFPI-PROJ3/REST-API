using System.Net;

namespace Proj3.Application.Common.Errors.Authentication;

public class InvalidPasswordException : Exception, IExceptionBase
{
    public HttpStatusCode StatusCode => HttpStatusCode.UnprocessableEntity;

    public string ErrorMessage => "Password is invalid.";
}
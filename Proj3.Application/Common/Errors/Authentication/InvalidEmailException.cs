using System.Net;

namespace Proj3.Application.Common.Errors.Authentication;

public class InvalidEmailException : Exception, IExceptionBase
{
    public HttpStatusCode StatusCode => HttpStatusCode.UnprocessableEntity;

    public string ErrorMessage => "This email is invalid.";
}
using System.Net;

namespace Proj3.Application.Common.Errors.Authentication;

public class UserNotFoundException : Exception, IExceptionBase
{
    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

    public string ErrorMessage => "User not found.";
}
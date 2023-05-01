using Microsoft.AspNetCore.Http;
using System.Net;

namespace Proj3.Application.Common.Errors.Authentication;

public class UserNotFoundException : Exception, IExceptionBase
{
    public int StatusCode => StatusCodes.Status404NotFound;

    public string ErrorMessage => "User not found.";
}
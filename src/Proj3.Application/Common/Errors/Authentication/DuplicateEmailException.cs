using Microsoft.AspNetCore.Http;
using System.Net;

namespace Proj3.Application.Common.Errors.Authentication;

public class DuplicateEmailException : Exception, IExceptionBase
{
    public int StatusCode => StatusCodes.Status409Conflict;

    public string ErrorMessage => "Email already exists.";
    
}
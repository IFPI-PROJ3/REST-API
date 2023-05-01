using Microsoft.AspNetCore.Http;
using System.Net;

namespace Proj3.Application.Common.Errors.Authentication;

public class DuplicatePhoneNumberInvalidCredentialsException : Exception, IExceptionBase
{
    public int StatusCode => StatusCodes.Status409Conflict;

    public string ErrorMessage => "Duplicate phone number.";
}
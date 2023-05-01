using Microsoft.AspNetCore.Http;
using System.Net;

namespace Proj3.Application.Common.Errors.NGO
{
    public class NgoAlreadyExistsException : Exception, IExceptionBase
    {
        public int StatusCode => StatusCodes.Status406NotAcceptable;

        public string ErrorMessage => "Ngo for this user already exists.";
    }
}

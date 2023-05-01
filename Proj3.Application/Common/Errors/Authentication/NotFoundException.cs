using Microsoft.AspNetCore.Http;

namespace Proj3.Application.Common.Errors.Authentication
{
    public class NotFoundException : Exception, IExceptionBase
    {
        public int StatusCode => StatusCodes.Status404NotFound;
        public string ErrorMessage => "Not found.";
    }
}

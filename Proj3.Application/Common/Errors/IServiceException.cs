using System.Net;

namespace Proj3.Application.Common.Errors
{
    public interface IExceptionBase
    {
        public HttpStatusCode StatusCode { get; }
        public string ErrorMessage { get; }
    }
}
namespace Proj3.Application.Common.Errors
{
    public interface IExceptionBase
    {
        public int StatusCode { get; }
        public string ErrorMessage { get; }
    }
}
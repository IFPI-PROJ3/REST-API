using System.Net;

namespace Proj3.Application.Common.Errors.Volunteer
{
    public class EventDateConflitException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.NotAcceptable;

        public string ErrorMessage => "Ngo for this user already exists.";
    }
}

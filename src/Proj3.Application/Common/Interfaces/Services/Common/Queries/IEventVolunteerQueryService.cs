using Microsoft.AspNetCore.Http;
using Proj3.Contracts.NGO.Response;

namespace Proj3.Application.Common.Interfaces.Services.Common.Queries
{
    public interface IEventVolunteerQueryService
    {        
        Task<List<EventRequestResponse>> GetRequestsByEvent(HttpContext httpContext, Guid eventId);

        Task<List<EventRequestResponse>> GetEventVolunteersByEvent(HttpContext httpContext, Guid eventId);
    }
}

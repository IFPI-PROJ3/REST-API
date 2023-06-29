using Microsoft.AspNetCore.Http;
using Proj3.Application.Common.Interfaces.Services.NGO.Commands;
using Proj3.Contracts.NGO.Request;
using Proj3.Contracts.NGO.Response;
using Proj3.Domain.Entities.NGO;

namespace Proj3.Application.Services.NGO.Commands
{
    public class EventCommandService : IEventCommandService
    {
        public EventCommandService() 
        { 
        }

        public Task<NewEventResponse> AddAsync(HttpContext httpContext, NewEventRequest newEventRequest)
        {
            throw new NotImplementedException();
        }

        public Task<UpdatedEventResponse> UpdateAsync(HttpContext httpContext, UpdateEventRequest updateEventRequest)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CancelAsync(HttpContext httpContext, Guid eventId)
        {
            throw new NotImplementedException();
        }
    }
}

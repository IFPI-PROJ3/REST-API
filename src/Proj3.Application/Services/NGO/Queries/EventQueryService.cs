using Microsoft.AspNetCore.Http;
using Proj3.Application.Common.DTO;
using Proj3.Application.Common.Interfaces.Services.NGO.Queries;
using Proj3.Contracts.NGO.Request;
using Proj3.Domain.Entities.NGO;

namespace Proj3.Application.Services.NGO.Queries
{
    public class EventQueryService : IEventQueryService
    {
        public EventQueryService()
        {
        }

        public IAsyncEnumerable<EventToFeed> GetEventsFeed(HttpContext httpContext, EventsFeedRequest eventsFeedRequest)
        {
            throw new NotImplementedException();
        }

        public Task<EventToPage> GetEventByIdAsync(HttpContext httpContext, Guid id)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<Event> GetAllByNgoAsync(HttpContext httpContext, Guid ngoId)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<Event> GetAllActiveByNgoAsync(HttpContext httpContext, Guid ngoId)
        {
            throw new NotImplementedException();
        }
    }
}

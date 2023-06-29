using Microsoft.AspNetCore.Http;
using Proj3.Application.Common.DTO;
using Proj3.Contracts.NGO.Request;
using Proj3.Domain.Entities.NGO;

namespace Proj3.Application.Common.Interfaces.Services.NGO.Queries
{
    public interface IEventQueryService
    {
        IAsyncEnumerable<EventToFeed> GetEventsFeed(HttpContext httpContext, EventsFeedRequest eventsFeedRequest);

        Task<EventToPage> GetEventByIdAsync(HttpContext httpContext, Guid id);

        IAsyncEnumerable<Event> GetAllByNgoAsync(HttpContext httpContext, Guid ngoId);

        IAsyncEnumerable<Event> GetAllActiveByNgoAsync(HttpContext httpContext, Guid ngoId);
    }
}

using Microsoft.AspNetCore.Http;
using Proj3.Contracts.NGO.Request;
using Proj3.Contracts.NGO.Response;

namespace Proj3.Application.Common.Interfaces.Services.NGO.Queries
{
    public interface IEventQueryService
    {
        Task<List<EventToCardVolunteer>> GetEventsFeedAsync(HttpContext httpContext /*EventsFeedRequest eventsFeedRequest*/);

        Task<List<EventToCard>> GetUpcomingEventsByNgoAsync(HttpContext httpContext, Guid ngoId);

        Task<List<EventToCard>> GetActiveEventsByNgoAsync(HttpContext httpContext, Guid ngoId);

        Task<List<EventToCard>> GetEndedEventsByNgoAsync(HttpContext httpContext, Guid ngoId);

        Task<EventToPage> GetEventByIdAsync(HttpContext httpContext, Guid eventId);

        Task<List<EventToCard>> GetAllByNgoAsync(HttpContext httpContext, Guid ngoId);        
    }
}

using Proj3.Domain.Entities.NGO;

namespace Proj3.Application.Common.Interfaces.Persistence.NGO
{
    public interface IEventRepository
    {
        IAsyncEnumerable<Event> GetAllByNgoAsync(Guid ngoId);

        Task<List<Event>> GetAllFeedAsync(Guid userId, int daysToEvent, List<int> categories);

        IAsyncEnumerable<Event> GetUpcomingEventsByNgoAsync(Guid ngoId);

        IAsyncEnumerable<Event> GetActiveEventsByNgoAsync(Guid ngoId);

        IAsyncEnumerable<Event> GetEndedEventsByNgoAsync(Guid ngoId);

        Task<List<Event>> GetUpcomingEventsByVolunteerAsync(Guid volunteerId);

        Task<List<Event>> GetActiveEventsByVolunteerAsync(Guid volunteerId);

        Task<List<Event>> GetEndedEventsByVolunteerAsync(Guid volunteerId);

        Task<Event?> GetEventByIdAsync(Guid eventId);

        Task<Event> AddAsync(Event @event);

        Task<bool> UpdateAsync(Event @event);

        Task CancelEventAsync(Guid eventId);
    }
}

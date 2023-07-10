using Proj3.Domain.Entities.Common;

namespace Proj3.Application.Common.Interfaces.Persistence.Common
{
    public interface IEventVolunteerRepository
    {
        public Task<List<EventVolunteer>> GetRequestsByEvent(Guid eventId);

        public Task<List<EventVolunteer>> GetEventVolunteersByEvent(Guid eventId);

        public Task<EventVolunteer?> GetEventVolunteerById(Guid eventVolunteerId);

        public Task AcceptRequestAsync(Guid eventId);

        public Task RefuseRequestAsync(Guid eventId);

        public Task NewRequestAsync(Guid eventId, Guid volunteerId);

        public Task DeleteRequestAsync(Guid eventId, Guid volunteerId);

        public Task<int> GetEventVolunteersCount(Guid eventId);

        public Task<int> GetEventRequestsCount(Guid eventId);
    }
}

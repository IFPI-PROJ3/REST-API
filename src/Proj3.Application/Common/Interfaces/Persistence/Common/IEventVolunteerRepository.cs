using Proj3.Domain.Entities.Common;

namespace Proj3.Application.Common.Interfaces.Persistence.Common
{
    public interface IEventVolunteerRepository
    {
        public Task<List<EventVolunteer>> GetRequestsByEvent(Guid eventId);

        public Task<List<EventVolunteer>> GetEventVolunteersByEvent(Guid eventId);

        public Task AcceptRequestAsync(Guid eventId);

        public Task RefuseRequestAsync(Guid eventId);

        public Task NewRequestAsync(Guid eventId, Guid volunteerId);

        public Task<int> GetEventParticipantsCount(Guid eventId);

        public Task<int> GetEventCandidatesCount(Guid eventId);
    }
}

using Microsoft.EntityFrameworkCore;
using Proj3.Application.Common.Interfaces.Persistence;
using Proj3.Application.Common.Interfaces.Persistence.Common;
using Proj3.Domain.Entities.Common;

namespace Proj3.Infrastructure.Persistence.Repositories.Common
{
    public class EventVolunteerRepository : IEventVolunteerRepository
    {
        private readonly IRepositoryBase<EventVolunteer> _repository;

        public EventVolunteerRepository(IRepositoryBase<EventVolunteer> repository)
        {
            _repository = repository;            
        }

        public async Task<List<EventVolunteer>> GetRequestsByEvent(Guid eventId)
        {
            return await _repository.Entity.Where(x => x.EventId == eventId && x.Accepted == null).ToListAsync();
        }

        public async Task<List<EventVolunteer>> GetEventVolunteersByEvent(Guid eventId)
        {
            return await _repository.Entity.Where(x => x.EventId == eventId && x.Accepted == true).ToListAsync();
        }

        public async Task AcceptRequestAsync(Guid eventVolunteerId)
        {
            var eventVolunteer = await _repository.Entity.Where(e => e.Id == eventVolunteerId).FirstOrDefaultAsync();
            eventVolunteer!.Accepted = true;
            await _repository.UpdateAsync(eventVolunteer);
        }

        public async Task RefuseRequestAsync(Guid eventVolunteerId)
        {
            var eventVolunteer = await _repository.Entity.Where(e => e.Id == eventVolunteerId).FirstOrDefaultAsync();
            eventVolunteer!.Accepted = false;
            await _repository.UpdateAsync(eventVolunteer);
        }

        public async Task NewRequestAsync(Guid eventId, Guid volunteerId)
        {
            await _repository.AddAsync(new EventVolunteer(eventId, volunteerId));
        }

        public async Task<int> GetEventParticipantsCount(Guid eventId)
        {
            return await _repository.Entity.Where(x => x.EventId == eventId && x.Accepted == true).CountAsync();
        }

        public async Task<int> GetEventCandidatesCount(Guid eventId)
        {
            var notresponse = await _repository.Entity.Where(x => x.EventId == eventId && x.Accepted == null).CountAsync();
            var accepted = await GetEventParticipantsCount(eventId);
            return notresponse + accepted;
        }
    }
}

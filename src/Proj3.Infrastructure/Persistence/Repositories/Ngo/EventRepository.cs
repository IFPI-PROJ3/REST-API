using Microsoft.EntityFrameworkCore;
using Proj3.Application.Common.Interfaces.Persistence;
using Proj3.Application.Common.Interfaces.Persistence.NGO;
using Proj3.Application.Common.Interfaces.Services;
using Proj3.Domain.Entities.NGO;

namespace Proj3.Infrastructure.Persistence.Repositories.Ngo
{
    public class EventRepository : IEventRepository
    {
        private readonly IRepositoryBase<Event> _repository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public EventRepository(IRepositoryBase<Event> repository)
        {
            _repository = repository;
        }

        public IAsyncEnumerable<Event> GetAllActive()
        {
            return _repository.Entity.Where(e => e.EndDate > _dateTimeProvider.UtcNow && e.Cancelled == false).AsAsyncEnumerable();
        }

        public IAsyncEnumerable<Event> GetAllActiveByNgo(Guid ngoId)
        {
            return _repository.Entity.Where(e => e.NgoId == ngoId && e.EndDate > _dateTimeProvider.UtcNow && e.Cancelled == false).AsAsyncEnumerable();
        }

        public IAsyncEnumerable<Event> GetAllByNgo(Guid ngoId)
        {
            return _repository.Entity.Where(e => e.NgoId == ngoId).AsAsyncEnumerable();
        }

        public Task<Event> AddAsync(Event @event)
        {
            return _repository.AddAsync(@event);
        }

        public Task<bool> UpdateAsync(Event @event)
        {
            return _repository.UpdateAsync(@event);
        }

        public Task<bool> DeleteAsync(Guid ngoId)
        {
            return _repository.DeleteAsync(ngoId);
        }
    }
}

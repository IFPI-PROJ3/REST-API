using Proj3.Domain.Entities.NGO;

namespace Proj3.Application.Common.Interfaces.Persistence.NGO
{
    public interface IEventRepository
    {
        IAsyncEnumerable<Event> GetAllByNgo(Guid ngoId);

        IAsyncEnumerable<Event> GetAllActiveByNgo(Guid ngoId);

        IAsyncEnumerable<Event> GetAllActive();

        Task<Event> AddAsync(Event @event);

        Task<bool> UpdateAsync(Event @event);

        Task<bool> DeleteAsync(Guid ngoId); 
    }
}

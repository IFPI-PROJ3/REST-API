using Proj3.Domain.Entities.NGO;

namespace Proj3.Application.Common.Interfaces.Services.NGO.Queries
{
    public interface IEventQueryService
    {
        IAsyncEnumerable<Event> GetAllByNgo(Guid ngoId);

        IAsyncEnumerable<Event> GetAllActiveByNgo(Guid ngoId);

        IAsyncEnumerable<Event> GetActiveByCategory(int categoyId);
    }
}

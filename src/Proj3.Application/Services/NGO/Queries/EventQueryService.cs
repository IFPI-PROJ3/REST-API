using Proj3.Application.Common.Interfaces.Services.NGO.Queries;
using Proj3.Domain.Entities.NGO;

namespace Proj3.Application.Services.NGO.Queries
{
    public class EventQueryService : IEventQueryService
    {
        public IAsyncEnumerable<Event> GetActiveByCategory(int categoyId)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<Event> GetAllActiveByNgo(Guid ngoId)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<Event> GetAllByNgo(Guid ngoId)
        {
            throw new NotImplementedException();
        }
    }
}

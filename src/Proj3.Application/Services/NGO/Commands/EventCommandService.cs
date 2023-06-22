using Proj3.Application.Common.Interfaces.Services.NGO.Commands;
using Proj3.Domain.Entities.NGO;

namespace Proj3.Application.Services.NGO.Commands
{
    public class EventCommandService : IEventCommandService
    {
        public Task<Event> Add(Event event_)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Guid ngoId)
        {
            throw new NotImplementedException();
        }

        public Task<Event> Update(Event event_)
        {
            throw new NotImplementedException();
        }
    }
}

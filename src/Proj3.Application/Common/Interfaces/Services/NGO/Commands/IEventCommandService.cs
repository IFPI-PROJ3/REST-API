using Proj3.Domain.Entities.NGO;

namespace Proj3.Application.Common.Interfaces.Services.NGO.Commands
{
    public interface IEventCommandService
    {
        Task<Event> Add(Event event_);

        Task<Event> Update(Event event_);

        Task<bool> Delete(Guid ngoId);
    }
}

using Proj3.Application.Common.DTO;
using Proj3.Contracts.NGO.Request;
using Proj3.Domain.Entities.NGO;

namespace Proj3.Application.Common.Interfaces.Services.NGO.Queries
{
    public interface INgoQueryService
    {
        Task<Ngo?> GetByUserIdAsync(Guid userId);

        Task<Ngo?> GetByIdAsync(Guid ngoId);

        Task<IEnumerable<NgoDTO>> AllNgosAsync(EventsFeedRequest searchOption);
    }
}

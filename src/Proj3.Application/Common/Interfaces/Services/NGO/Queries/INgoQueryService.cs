using Proj3.Application.Common.DTO;
using Proj3.Contracts.NGO.Request;
using Proj3.Domain.Entities.NGO;

namespace Proj3.Application.Common.Interfaces.Services.NGO.Queries
{
    public interface INgoQueryService
    {
        Task<Ngo?> GetByUserId(Guid userId);
        Task<Ngo?> GetById(Guid ngoId);
        Task<IEnumerable<NgoDTO>> AllNgos(EventsFeedRequest searchOption);
    }
}

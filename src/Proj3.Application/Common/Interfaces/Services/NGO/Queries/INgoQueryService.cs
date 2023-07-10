using Proj3.Application.Common.DTO;
using Proj3.Contracts.NGO.Request;
using Proj3.Contracts.NGO.Response;
using Proj3.Domain.Entities.NGO;

namespace Proj3.Application.Common.Interfaces.Services.NGO.Queries
{
    public interface INgoQueryService
    {
        Task<NgoPageInfo> GetNgoPage();

        Task<Ngo?> GetByUserIdAsync(Guid userId);

        Task<Ngo?> GetByIdAsync(Guid ngoId);        
    }
}

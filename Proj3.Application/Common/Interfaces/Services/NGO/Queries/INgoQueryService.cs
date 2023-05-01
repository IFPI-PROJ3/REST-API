using Proj3.Application.Common.DTO;
using Proj3.Contracts.NGO.Request;

namespace Proj3.Application.Common.Interfaces.Services.NGO.Queries
{
    public interface INgoQueryService
    {        
        Task<IEnumerable<NgoDTO>> AllNgos(SearchOptionsRequest searchOption);
    }
}

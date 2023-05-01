using Proj3.Application.Common.DTO;
using Proj3.Application.Common.Interfaces.Persistence.NGO;
using Proj3.Application.Common.Interfaces.Services.NGO.Queries;
using Proj3.Contracts.NGO.Request;

namespace Proj3.Application.Services.NGO.Queries
{
    public class NgoQueryService : INgoQueryService
    {
        private readonly INgoRepository _ngoRepository;

        public NgoQueryService(INgoRepository ngoRepository)
        {
            _ngoRepository = ngoRepository;
        }

        public Task<IEnumerable<NgoDTO>> AllNgos(SearchOptionsRequest searchOption)
        {
            throw new NotImplementedException();
        }
    }
}

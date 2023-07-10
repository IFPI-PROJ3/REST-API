using Proj3.Application.Common.Interfaces.Persistence.NGO;
using Proj3.Application.Common.Interfaces.Services.NGO.Queries;
using Proj3.Contracts.NGO.Response;
using Proj3.Domain.Entities.NGO;

namespace Proj3.Application.Services.NGO.Queries
{
    public class NgoQueryService : INgoQueryService
    {
        private readonly INgoRepository _ngoRepository;                

        public NgoQueryService(INgoRepository ngoRepository)
        {
            _ngoRepository = ngoRepository;            
        }

        public Task<NgoPageInfo> GetNgoPage()
        {
            throw new NotImplementedException();
        }

        public async Task<Ngo?> GetByUserIdAsync(Guid userId)
        {
            return await _ngoRepository.GetByUserId(userId);
        }

        public async Task<Ngo?> GetByIdAsync(Guid ngoId)
        {
            return await _ngoRepository.GetByIdAsync(ngoId);
        }        
    }
}

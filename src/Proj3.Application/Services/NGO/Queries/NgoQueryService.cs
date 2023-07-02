using Proj3.Application.Common.DTO;
using Proj3.Application.Common.Interfaces.Persistence.NGO;
using Proj3.Application.Common.Interfaces.Services.NGO.Queries;
using Proj3.Contracts.NGO.Request;
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

        public async Task<Ngo?> GetByUserIdAsync(Guid userId)
        {
            return await _ngoRepository.GetByUserId(userId);
        }

        public async Task<Ngo?> GetByIdAsync(Guid ngoId)
        {
            return await _ngoRepository.GetByIdAsync(ngoId);
        }        

        public Task<IEnumerable<NgoDTO>> AllNgosAsync(EventsFeedRequest searchOption)
        {
            throw new NotImplementedException();
        }
    }
}

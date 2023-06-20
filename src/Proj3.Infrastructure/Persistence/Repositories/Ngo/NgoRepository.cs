using Microsoft.EntityFrameworkCore;
using Proj3.Application.Common.Interfaces.Persistence;
using Proj3.Application.Common.Interfaces.Persistence.NGO;

namespace Proj3.Infrastructure.Persistence.Repositories.Ngo
{
    public class NgoRepository : INgoRepository
    {
        private readonly IRepositoryBase<Domain.Entities.NGO.Ngo> _repository;

        public NgoRepository(IRepositoryBase<Domain.Entities.NGO.Ngo> repository)
        {
            _repository = repository;
        }
        public async Task<Domain.Entities.NGO.Ngo?> GetByUserId(Guid userId)
        {
            return await _repository.Entity.Where(n => n.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task<Domain.Entities.NGO.Ngo?> GetById(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Domain.Entities.NGO.Ngo> Add(Domain.Entities.NGO.Ngo ngo)
        {
            return await _repository.AddAsync(ngo);
        }

        public async Task<Domain.Entities.NGO.Ngo> Update(Domain.Entities.NGO.Ngo ngo)
        {
            await _repository.UpdateAsync(ngo);
            return ngo;
        }
    }
}

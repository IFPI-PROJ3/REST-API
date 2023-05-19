using Microsoft.EntityFrameworkCore;
using Proj3.Application.Common.Interfaces.Persistence;
using Proj3.Application.Common.Interfaces.Persistence.NGO;
using Proj3.Domain.Entities.NGO;

namespace Proj3.Infrastructure.Repositories.NGO
{
    public class NgoRepository : INgoRepository
    {
        private readonly IRepositoryBase<Ngo> _repository;

        public NgoRepository(IRepositoryBase<Ngo> repository)
        {
            _repository = repository;
        }

        public async Task<Ngo> Add(Ngo ngo)
        {
            return await _repository.AddAsync(ngo);
        }

        public async Task<Ngo> Update(Ngo ngo)
        {
            await _repository.UpdateAsync(ngo);
            return ngo;
        }

        public async Task<bool> UserNgoAlreadyExists(Guid userId)
        {
            return await _repository.Entity.Where(n => n.UserId == userId).AnyAsync();
        }
    }
}

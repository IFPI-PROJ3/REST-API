using Microsoft.EntityFrameworkCore;
using Proj3.Application.Common.Interfaces.Persistence;
using Proj3.Application.Common.Interfaces.Persistence.NGO;

namespace Proj3.Infrastructure.Persistence.Repositories.NGO
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

        public async Task<List<Domain.Entities.NGO.Ngo>> GetByCategoriesAsync(List<int> categoriesId)
        {
            var context = _repository.Context as AppDbContext;
            List<Guid> ngoIds = new();
            List<Domain.Entities.NGO.Ngo>? ngos = new();

            foreach (int categoryId in categoriesId)
            {
                ngoIds.AddRange(await context!.NgoCategories!.Where(nc => nc.CategoryId == categoryId).Select(nc => nc.NgoId).ToListAsync());
            }

            foreach(Guid ngoId in ngoIds)
            {
                ngos.Add((await GetByIdAsync(ngoId))!);
            }

            return ngos;
        }

        public async Task<Domain.Entities.NGO.Ngo?> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Domain.Entities.NGO.Ngo> AddAsync(Domain.Entities.NGO.Ngo ngo)
        {
            return await _repository.AddAsync(ngo);
        }

        public async Task<Domain.Entities.NGO.Ngo> UpdateAsync(Domain.Entities.NGO.Ngo ngo)
        {
            await _repository.UpdateAsync(ngo);
            return ngo;
        }
    }
}

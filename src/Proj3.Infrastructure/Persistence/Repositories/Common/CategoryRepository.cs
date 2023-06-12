using Proj3.Application.Common.Interfaces.Persistence;
using Proj3.Application.Common.Interfaces.Persistence.Common;
using Proj3.Domain.Entities.Common;
using Proj3.Domain.Entities.NGO;
using Proj3.Domain.Entities.Volunteer;

namespace Proj3.Infrastructure.Persistence.Repositories.Common
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IRepositoryBase<Category> _repository;
        private readonly IRepositoryBase<NgoCategory> _repositoryNgoCategory;
        private readonly IRepositoryBase<VolunteerCategory> _repositoryVolunteerCategory;

        public CategoryRepository(IRepositoryBase<Category> repository, 
            IRepositoryBase<NgoCategory> repositoryNgoCategory,
            IRepositoryBase<VolunteerCategory> repositoryVolunteerCategory)
        {
            _repository = repository;
            _repositoryNgoCategory = repositoryNgoCategory;
            _repositoryVolunteerCategory = repositoryVolunteerCategory;
        }

        public IAsyncEnumerable<Category> GetAll()
        {
            return _repository.GetAllAsync();
        }

        public async Task AddCategoriesToNgoAsync(Guid ngoId, List<int> categories)
        {
            foreach(var categoryId in categories)
            {
                await _repositoryNgoCategory.AddAsync(new NgoCategory(ngoId, categoryId));
            }
        }

        public async Task AddCategoriesToVolunteerAsync(Guid ngoId, List<int> categories)
        {
            foreach (var categoryId in categories)
            {
                await _repositoryVolunteerCategory.AddAsync(new VolunteerCategory(ngoId, categoryId));
            }
        }
    }
}

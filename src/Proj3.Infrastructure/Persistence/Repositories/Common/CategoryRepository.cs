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

        public IAsyncEnumerable<Category> GetAllAsync()
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

        public async Task<List<string>> GetAllCategoriesByNgoAsync(Guid ngoId)
        {
            List<string> categoriesStrArray = new();

            var categories = _repositoryNgoCategory.Entity.Where(c => c.NgoId == ngoId).ToList();
            
            foreach(var ngoCategory in categories)
            {
                var category = await _repository.GetByIdAsync(ngoCategory.CategoryId);
                categoriesStrArray.Add(category.Name);
            }
            
            return categoriesStrArray;
        }

        public async Task<List<string>> GetAllCategoriesByVolunteerAsync(Guid volunteerId)
        {
            List<string> categoriesStrArray = new();

            var categories = _repositoryVolunteerCategory.Entity.Where(c => c.VolunteerId == volunteerId).ToList();

            foreach (var volunteerCategory in categories)
            {
                var category = await _repository.GetByIdAsync(volunteerCategory.CategoryId);
                categoriesStrArray.Append(category.Name);
            }

            return categoriesStrArray;
        }
    }
}

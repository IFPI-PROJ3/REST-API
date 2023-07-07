using Proj3.Application.Common.Interfaces.Persistence.Common;
using Proj3.Application.Common.Interfaces.Services.Common.Queries;
using Proj3.Domain.Entities.Common;

namespace Proj3.Application.Services.Common.Queries
{
    public class CategoryQueryService : ICategoryQueryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryQueryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IAsyncEnumerable<Category> GetAllAsync()
        {
            return _categoryRepository.GetAllAsync();
        }

        public Task<List<string>> GetCategoryNameByNgoAsync(Guid ngoId)
        {
            return _categoryRepository.GetAllCategoriesByNgoAsync(ngoId);
        }

        public Task<List<string>> GetCategoryNameByVolunteerAsync(Guid volunteerId)
        {
            return _categoryRepository.GetAllCategoriesByVolunteerAsync(volunteerId);
        }
    }
}

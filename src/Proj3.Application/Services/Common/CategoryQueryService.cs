using Proj3.Application.Common.Interfaces.Persistence.Common;
using Proj3.Application.Common.Interfaces.Services.Common.Queries;
using Proj3.Domain.Entities.Common;

namespace Proj3.Application.Services.Common
{
    public class CategoryQueryService : ICategoryQueryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryQueryService(ICategoryRepository categoryRepository) 
        {
            _categoryRepository = categoryRepository;
        }

        public IAsyncEnumerable<Category> GetAll()
        {
            return _categoryRepository.GetAll();
        }

        public Task<List<string>> GetCategoryNameByNgo(Guid ngoId)
        {
            return _categoryRepository.GetAllCategoriesByNgo(ngoId);
        }

        public Task<List<string>> GetCategoryNameByVolunteer(Guid volunteerId)
        {
            return _categoryRepository.GetAllCategoriesByVolunteer(volunteerId);
        }
    }
}

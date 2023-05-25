using Proj3.Application.Common.Interfaces.Persistence.Common;
using Proj3.Application.Common.Interfaces.Services.Common;
using Proj3.Domain.Entities.Common;

namespace Proj3.Application.Services.Common
{
    public class CategoryQueryService : ICategoryQueryService
    {
        public readonly ICategoryRepository _categoryRepository;

        public CategoryQueryService(ICategoryRepository categoryRepository) 
        {
            _categoryRepository = categoryRepository;
        }

        public IAsyncEnumerable<Category> GetAll()
        {
            return _categoryRepository.GetAll();
        }
    }
}

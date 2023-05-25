using Proj3.Application.Common.Interfaces.Persistence;
using Proj3.Application.Common.Interfaces.Persistence.Common;
using Proj3.Domain.Entities.Common;

namespace Proj3.Infrastructure.Persistence.Repositories.Common
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IRepositoryBase<Category> _repository;

        public CategoryRepository(IRepositoryBase<Category> repository)
        {
            _repository = repository;
        }

        public IAsyncEnumerable<Category> GetAll()
        {
            return _repository.GetAllAsync();
        }
    }
}

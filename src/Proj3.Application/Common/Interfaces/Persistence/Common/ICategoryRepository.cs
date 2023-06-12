using Proj3.Domain.Entities.Common;

namespace Proj3.Application.Common.Interfaces.Persistence.Common
{
    public interface ICategoryRepository
    {
        IAsyncEnumerable<Category> GetAll();

        Task AddCategoriesToNgoAsync(Guid ngoId, List<int> categories);

        Task AddCategoriesToVolunteerAsync(Guid ngoId, List<int> categories);
    }
}

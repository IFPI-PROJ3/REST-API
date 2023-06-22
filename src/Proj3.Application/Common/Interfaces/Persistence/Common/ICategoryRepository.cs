using Proj3.Domain.Entities.Common;

namespace Proj3.Application.Common.Interfaces.Persistence.Common
{
    public interface ICategoryRepository
    {
        IAsyncEnumerable<Category> GetAll();

        Task<List<string>> GetAllCategoriesByNgo(Guid ngoId);

        Task<List<string>> GetAllCategoriesByVolunteer(Guid ngoId);

        Task AddCategoriesToNgoAsync(Guid ngoId, List<int> categories);

        Task AddCategoriesToVolunteerAsync(Guid ngoId, List<int> categories);
    }
}

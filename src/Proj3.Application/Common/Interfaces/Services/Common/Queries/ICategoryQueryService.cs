using Proj3.Domain.Entities.Common;

namespace Proj3.Application.Common.Interfaces.Services.Common.Queries
{
    public interface ICategoryQueryService
    {
        IAsyncEnumerable<Category> GetAllAsync();

        Task<List<string>> GetCategoryNameByNgoAsync(Guid ngoId);

        Task<List<string>> GetCategoryNameByVolunteerAsync(Guid volunteerId);
    }
}

using Proj3.Domain.Entities.Common;

namespace Proj3.Application.Common.Interfaces.Services.Common.Queries
{
    public interface ICategoryQueryService
    {
        IAsyncEnumerable<Category> GetAll();

        Task<List<string>> GetCategoryNameByNgo(Guid ngoId);

        Task<List<string>> GetCategoryNameByVolunteer(Guid volunteerId);
    }
}

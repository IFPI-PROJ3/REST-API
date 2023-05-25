using Proj3.Domain.Entities.Common;

namespace Proj3.Application.Common.Interfaces.Services.Common
{
    public interface ICategoryQueryService
    {
        IAsyncEnumerable<Category> GetAll();
    }
}

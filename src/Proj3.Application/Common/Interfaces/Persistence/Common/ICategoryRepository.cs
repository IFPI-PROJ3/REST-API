using Proj3.Domain.Entities.Common;

namespace Proj3.Application.Common.Interfaces.Persistence.Common
{
    public interface ICategoryRepository
    {
        IAsyncEnumerable<Category> GetAll();
    }
}

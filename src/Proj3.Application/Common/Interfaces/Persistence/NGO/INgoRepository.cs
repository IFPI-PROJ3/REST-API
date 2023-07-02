using Proj3.Domain.Entities.NGO;

namespace Proj3.Application.Common.Interfaces.Persistence.NGO
{
    public interface INgoRepository
    {
        Task<Ngo?> GetByUserId(Guid userId);

        Task<List<Ngo>> GetByCategoriesAsync(List<int> categoriesId);

        Task<Ngo?> GetByIdAsync(Guid ngoId);

        Task<Ngo> AddAsync(Ngo ngo);

        Task<Ngo> UpdateAsync(Ngo ngo);        
    }
}

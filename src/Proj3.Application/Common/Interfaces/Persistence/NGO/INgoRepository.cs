using Proj3.Domain.Entities.NGO;

namespace Proj3.Application.Common.Interfaces.Persistence.NGO
{
    public interface INgoRepository
    {
        Task<Ngo?> GetByUserId(Guid userId);
        Task<Ngo?> GetById(Guid ngoId);
        Task<Ngo> Add(Ngo ngo);
        Task<Ngo> Update(Ngo ngo);        
    }
}

using Proj3.Domain.Entities.Authentication;

namespace Proj3.Application.Common.Interfaces.Persistence.Authentication
{
    public interface IUserRepository
    {
        Task<User> AddAsync(User user);
        Task<User> UpdateAsync(User user);
        Task<User?> GetUserByEmailAsync(string email);        
        Task<User?> GetUserByIdAsync(Guid id);
    }
}
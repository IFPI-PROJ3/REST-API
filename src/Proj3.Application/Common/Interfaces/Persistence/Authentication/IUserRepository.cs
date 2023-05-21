using Proj3.Domain.Entities.Authentication;

namespace Proj3.Application.Common.Interfaces.Persistence.Authentication
{
    public interface IUserRepository
    {
        Task<User> Add(User user);
        Task<User> Update(User user);
        Task<User?> GetUserByEmail(string email);        
        Task<User?> GetUserById(Guid id);
    }
}
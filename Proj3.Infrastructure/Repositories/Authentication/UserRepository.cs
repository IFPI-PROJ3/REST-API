using Microsoft.EntityFrameworkCore;
using Proj3.Application.Common.Interfaces.Persistence.Authentication;
using Proj3.Domain.Entities.Authentication;
using Proj3.Infrastructure.Database;

namespace Proj3.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbcontext;

        public UserRepository(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task Add(User user)
        {
            await _dbcontext.Users!.AddAsync(user);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<User> Update(User user)
        {
            User updateUser = _dbcontext.Users!.SingleOrDefault(u => u == user)!;

            // MUTABLES
            updateUser.UserName = user.UserName;            
            updateUser.PasswordHash = user.PasswordHash;
            updateUser.Active = user.Active;

            await _dbcontext.SaveChangesAsync();

            return updateUser;
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _dbcontext.Users!.Where(user => user.Email == email).SingleOrDefaultAsync();
        }

        public async Task<User?> GetUserById(Guid id)
        {
            return await _dbcontext.Users!.Where(user => user.Id == id).SingleOrDefaultAsync();
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Proj3.Application.Common.Interfaces.Persistence;
using Proj3.Application.Common.Interfaces.Persistence.Authentication;
using Proj3.Domain.Entities.Authentication;

namespace Proj3.Infrastructure.Persistence.Repositories.Authentication
{
    public class UserRepository : IUserRepository
    {
        private readonly IRepositoryBase<User> _repository;

        public UserRepository(IRepositoryBase<User> repository)
        {
            _repository = repository;
        }

        public async Task<User> Add(User user)
        {
            return await _repository.AddAsync(user);
        }
        public async Task<User> Update(User user)
        {
            await _repository.UpdateAsync(user);
            return user;
        }

        public async Task<User?> GetUserById(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _repository.Entity.Where(user => user.Email == email).SingleOrDefaultAsync();
        }
    }
}
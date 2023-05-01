using Microsoft.EntityFrameworkCore;
using Proj3.Application.Common.Interfaces.Persistence;
using Proj3.Application.Common.Interfaces.Persistence.Authentication;
using Proj3.Domain.Entities.Authentication;

namespace Proj3.Infrastructure.Repositories
{
    public class UserValidationCodeRepository : IUserValidationCodeRepository
    {
        private readonly IRepositoryBase<UserValidationCode> _repository;

        public UserValidationCodeRepository(IRepositoryBase<UserValidationCode> repository)
        {
            _repository = repository;
        }

        public async Task Add(UserValidationCode userValidationCode)
        {
            await _repository.AddAsync(userValidationCode);
        }

        public async Task RemoveUserConfirmation(UserValidationCode userValidationCode)
        {
            await _repository.DeleteAsync(userValidationCode.Id);            
        }

        public async Task<UserValidationCode?> GetEmailValidationCodeByUser(User user)
        {            
            return await _repository.Entity.Where(userCode => userCode.UserId == user.Id).SingleOrDefaultAsync();
        }

        public async Task RenewCode(UserValidationCode userValidationCode)
        {
            UserValidationCode? renewUserValidationCode = await _repository.GetByIdAsync(userValidationCode.Id);
            renewUserValidationCode!.RenewUserValidationCode();
            await _repository.UpdateAsync(renewUserValidationCode);
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Proj3.Application.Common.Interfaces.Persistence.Authentication;
using Proj3.Domain.Entities.Authentication;
using Proj3.Infrastructure.Database;
namespace Proj3.Infrastructure.Repositories
{
    public class UserValidationCodeRepository : IUserValidationCodeRepository
    {
        private readonly AppDbContext _dbcontext;

        public UserValidationCodeRepository(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task Add(UserValidationCode userValidationCode)
        {
            await _dbcontext.UserValidationCodes!.AddAsync(userValidationCode);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task RemoveUserConfirmation(UserValidationCode userValidationCode)
        {
            _dbcontext.UserValidationCodes!.Remove(userValidationCode);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<UserValidationCode?> GetEmailValidationCodeByUser(User user)
        {
            return await _dbcontext.UserValidationCodes!.Where(u => u.UserId == user.Id && u.Type.Contains("@")).SingleOrDefaultAsync();
        }

        public async Task<UserValidationCode?> GetPhoneNumberValidationCodeByUser(User user)
        {

            return await _dbcontext.UserValidationCodes!.Where(u => u.UserId == user.Id && !u.Type.Contains("@")).SingleOrDefaultAsync();
        }

        public async Task RenewCode(UserValidationCode userValidationCode)
        {
            UserValidationCode? renewUserValidationCode = await _dbcontext.UserValidationCodes!.SingleOrDefaultAsync(u => u == userValidationCode);
            renewUserValidationCode!.RenewUserValidationCode();
            await _dbcontext.SaveChangesAsync();
        }
    }
}
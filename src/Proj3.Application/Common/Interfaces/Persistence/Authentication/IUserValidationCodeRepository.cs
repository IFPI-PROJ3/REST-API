using Proj3.Domain.Entities.Authentication;

namespace Proj3.Application.Common.Interfaces.Persistence.Authentication
{
    public interface IUserValidationCodeRepository
    {
        Task AddAsync(UserValidationCode userValidationCode);

        Task<UserValidationCode?> GetEmailValidationCodeByUserAsync(User user);  
        
        Task RemoveUserConfirmationAsync(UserValidationCode userValidationCode);

        Task RenewCodeAsync(UserValidationCode userValidationCode);
    }
}
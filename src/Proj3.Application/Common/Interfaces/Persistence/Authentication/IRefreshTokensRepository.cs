using Proj3.Domain.Entities.Authentication;

namespace Proj3.Application.Common.Interfaces.Persistence.Authentication
{
    public interface IRefreshTokenRepository
    {
        Task AddAsync(RefreshToken rf);        

        Task RemoveAsync(RefreshToken rf);

        Task<IEnumerable<RefreshToken>> GetAllUsersRefreshTokensAsync(Guid userId);

        Task<RefreshToken?> GetByTokenAsync(string token);

        Task RevokeAllTokensFromUserAsync(Guid userId);

        Task<bool> ValidateIatTokenAsync(Guid userId, string iat);
    }
}

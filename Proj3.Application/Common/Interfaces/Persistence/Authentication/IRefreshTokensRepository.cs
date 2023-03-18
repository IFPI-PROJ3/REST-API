﻿using Proj3.Domain.Entities.Authentication;

namespace Proj3.Application.Common.Interfaces.Persistence.Authentication
{
    public interface IRefreshTokenRepository
    {
        Task Add(RefreshToken rf);

        Task<RefreshToken> Update(RefreshToken rf);

        Task Remove(RefreshToken rf);

        Task<IEnumerable<RefreshToken>> GetAllUsersRefreshTokens(Guid userId);

        Task<RefreshToken?> GetByToken(string token);

        Task RevokeAllTokensFromUser(Guid userId);

        Task<bool> ValidateIatToken(Guid userId, string iat);
    }
}

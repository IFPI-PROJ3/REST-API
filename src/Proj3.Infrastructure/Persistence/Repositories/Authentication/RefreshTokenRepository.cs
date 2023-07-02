using Microsoft.EntityFrameworkCore;
using Proj3.Application.Common.Interfaces.Persistence;
using Proj3.Application.Common.Interfaces.Persistence.Authentication;
using Proj3.Domain.Entities.Authentication;

namespace Proj3.Infrastructure.Persistence.Repositories.Authentication
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        //private readonly AppDbContext _dbcontext;
        private readonly IRepositoryBase<RefreshToken> _repository;

        public RefreshTokenRepository(IRepositoryBase<RefreshToken> repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(RefreshToken rf)
        {
            // se nao existir no banco adiciona o token se existir atualiza.            
            // posteriormente alterar para mais dispositivos podendo ter mais de um token por usuario            
            if (await _repository.Entity.Where(r => r.UserId == rf.UserId).AnyAsync())
            {
                await Update(rf);
            }
            else
            {
                await _repository.AddAsync(rf);
            }            
        }

        private async Task<RefreshToken> Update(RefreshToken rf)
        {            
            RefreshToken updateRefreshToken = (await _repository.Entity.Where(r => r.UserId == rf.UserId).SingleOrDefaultAsync())!;

            updateRefreshToken.Token = rf.Token;
            updateRefreshToken.Created = rf.Created;
            updateRefreshToken.Expires = rf.Expires;
            updateRefreshToken.Iat = rf.Iat;
            
            await _repository.UpdateAsync(updateRefreshToken);

            return updateRefreshToken;
        }

        public async Task RemoveAsync(RefreshToken rf)
        {
            await _repository.DeleteAsync(rf.Id);            
        }

        public async Task<IEnumerable<RefreshToken>> GetAllUsersRefreshTokensAsync(Guid userId)
        {            
            IEnumerable<RefreshToken> list = await _repository.Entity.Where(r => r.UserId == userId).ToListAsync();
            return list;
        }

        public async Task<RefreshToken?> GetByTokenAsync(string token)
        {            
            RefreshToken? refreshToken = await _repository.Entity.Where(r => r.Token == token).SingleOrDefaultAsync();
            return refreshToken;
        }

        public async Task RevokeAllTokensFromUserAsync(Guid userId)
        {
            IEnumerable<RefreshToken> list = await GetAllUsersRefreshTokensAsync(userId);
            // ### REFATORAR PARA FUNCAO EM REPOSITORYBASE QUE REMOVA O RANGE
            _repository.Entity.RemoveRange(list);
            await _repository.Context.SaveChangesAsync();            
        }

        public async Task<bool> ValidateIatTokenAsync(Guid userId, string iat)
        {            
            return await _repository.Entity.Where(r => r.UserId == userId && r.Iat == iat).AnyAsync();
        }
    }
}

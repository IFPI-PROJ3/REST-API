using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Proj3.Application.Common.Interfaces.Persistence;

namespace Proj3.Infrastructure.Repositories
{
    /// <summary>
    /// Class <c>RepositoryBase<T></c> implementação padrao de <see cref="IRepositoryBase{T}"/>.
    /// </summary>
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        public DbSet<TEntity> Entity { get; }

        public DbContext Context { get; }

        public RepositoryBase(DbContext dbContext)
        {
            Context = dbContext;
            Entity = dbContext.Set<TEntity>();
        }

        public IAsyncEnumerable<TEntity> GetAllAsync()
        {
            return Entity.AsAsyncEnumerable();
        }

        public async Task<TEntity?> GetByIdAsync(object id)
        {
            return await Entity.FindAsync(id);
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await ChangeStateAndSaveAsync(entity, EntityState.Added);
            return entity;
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            return await ChangeStateAndSaveAsync(entity, EntityState.Modified);
        }

        public async Task<bool> DeleteAsync(object id)
        {
            if (await Entity.FindAsync(id) is TEntity entity)
            {
                return await ChangeStateAndSaveAsync(entity, EntityState.Deleted);
            }
            else
            {
                return false;
            }
        }

        public IQueryable<TEntity> QueryNoTracking()
        {
            return Entity.AsNoTracking();
        }

        public async Task ReloadIfModifiedAsync(TEntity entity)
        {
            EntityEntry<TEntity> entry = Context.Entry(entity);
            if (entry.State == EntityState.Modified)
            {
                await entry.ReloadAsync();
            }
        }

        private async Task<bool> ChangeStateAndSaveAsync(TEntity entity, EntityState state)
        {
            ChangeState(entity, state);
            return await Context.SaveChangesAsync() > 0;
        }

        private void ChangeState(TEntity entity, EntityState state)
        {
            EntityEntry<TEntity> entry = Context.Entry(entity);
            entry.State = state;
        }
    }
}

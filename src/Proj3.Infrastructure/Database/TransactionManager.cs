using Microsoft.EntityFrameworkCore;
using Proj3.Application.Common.Interfaces.Others;

namespace Proj3.Infrastructure.Database
{
    public class TransactionManager : ITransactionsManager
    {
        private readonly DbContext _dbContext;

        public TransactionManager(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task BeginTransactionAsync()
        {
            await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await _dbContext.Database.CommitTransactionAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await _dbContext.Database.RollbackTransactionAsync();
        }
    }
}

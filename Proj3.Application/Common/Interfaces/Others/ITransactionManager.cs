using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj3.Application.Common.Interfaces.Others
{
    /// <summary>
    /// Provides methods to enable transaction support.
    /// </summary>
    public interface ITransactionsManager
    {
        /// <summary>
        /// Initiates a transaction scope.
        /// </summary>
        Task BeginTransactionAsync();

        /// <summary>
        /// Executes the transaction.
        /// </summary>
        Task CommitTransactionAsync();

        /// <summary>
        /// Rollback the transaction.
        /// </summary>
        Task RollbackTransactionAsync();
    }
}

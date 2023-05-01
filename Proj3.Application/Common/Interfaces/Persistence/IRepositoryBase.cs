using Microsoft.EntityFrameworkCore;

namespace Proj3.Application.Common.Interfaces.Persistence
{
    /// <summary>
    /// Interface <c>IRepositoryBase<T></c> para repositório base genérico
    /// </summary>
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        /// <summary>
        /// Método de leitura para retornar o dbset da tabela
        /// </summary>        
        /// <returns>Retorna uma coleção do tipo IEnumerable contendo todos os dados da tabela</returns>        
        DbSet<TEntity> Entity { get; }

        public DbContext Context { get; }

        /// <summary>
        /// Método de leitura assíncrono para retornar todos os dados da tabela
        /// </summary>        
        /// <returns>Retorna uma coleção do tipo IEnumerable contendo todos os dados da tabela</returns>        
        IAsyncEnumerable<TEntity> GetAllAsync();

        /// <summary>
        /// Método de leitura assíncrono para retornar um uníco objeto        
        /// </summary>        
        /// <param name="id">Identificador único do objeto na tabela</param>
        /// <returns>Retorna um objeto, caso não encontre retorna null</returns>
        Task<TEntity?> GetByIdAsync(object id);

        /// <summary>
        /// Método de comando assíncrono para inserir um objeto na tabela
        /// </summary>        
        /// <param name="entity">Objeto que será inserido</param>
        /// <returns>Retorna que foi inserido na tabela</returns>
        Task<TEntity> AddAsync(TEntity entity);

        /// <summary>
        /// Método de comando assíncrono para atualizar um objeto na tabela
        /// </summary>        
        /// <param name="entity">Objeto que será atualizado</param>
        /// <returns>Retorna que foi atualizado na tabela</returns>
        Task<bool> UpdateAsync(TEntity entity);

        /// <summary>
        /// Método de comando assíncrono para remover um objeto na tabela
        /// </summary>        
        /// <param name="id">Identificador único do objeto na tabela</param>
        /// <returns>Retorna um booleano como resultado da remoção</returns>
        Task<bool> DeleteAsync(object id);

        /// <summary>
        /// Método de leitura assíncrono para retornar query sem o tracking de mudanças do EF
        /// </summary>                
        /// <returns>Retorna um IQueryable</returns>
        IQueryable<TEntity> QueryNoTracking();

        /// <summary>
        /// Método de leitura assíncrono para retornar recarregar o objeto para o estado original
        /// </summary>                
        /// <returns>Retorna um IQueryable</returns>
        Task ReloadIfModifiedAsync(TEntity entity);
    }
}
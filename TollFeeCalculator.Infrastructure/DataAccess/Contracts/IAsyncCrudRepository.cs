using System.Linq.Expressions;
using TollFeeCalculator.Domain.Contracts;

namespace TollFeeCalculator.Infrastructure.DataAccess.Contracts;

/// <summary>
/// Base contract of database operations of repositories
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IAsyncCrudRepository<TEntity> 
    where TEntity : class, IEntity
{
    /// <summary>
    /// Adds an entity
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TEntity> Add(
        TEntity entity, 
        CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Updates an entity
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TEntity> Update(
        TEntity entity, 
        CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Deletes an entity
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> Delete(
        TEntity entity, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets first item of entity
    /// </summary>
    /// <param name="whereExpression"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TEntity?> GetFirst(
        Expression<Func<TEntity, bool>> whereExpression, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets counts of occurrence of items
    /// </summary>
    /// <param name="whereExpression"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<int> GetCount(
        Expression<Func<TEntity, bool>> whereExpression, 
        CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets a list of TEntity
    /// </summary>
    /// <param name="whereExpression"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IReadOnlyList<TEntity>> GetList(
        Expression<Func<TEntity, bool>>? whereExpression = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a list of <see cref="TResult"/> with running query
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="TResult"></typeparam>
    /// <returns></returns>
    Task<IReadOnlyList<TResult>> GetList<TResult>(
        string query,
        CancellationToken cancellationToken = default) where TResult : class;
}
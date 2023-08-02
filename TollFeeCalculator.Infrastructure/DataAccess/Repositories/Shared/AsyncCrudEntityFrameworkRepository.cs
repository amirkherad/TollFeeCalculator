using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TollFeeCalculator.Domain.Contracts;
using TollFeeCalculator.Infrastructure.DataAccess.Contracts;

namespace TollFeeCalculator.Infrastructure.DataAccess.Repositories.Shared;

/// <summary>
/// The generic implemented methods of IAsyncRepository for crud operations of entities base on entity framework orm  
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public class AsyncCrudEntityFrameworkRepository<TEntity> 
    : IAsyncCrudRepository<TEntity> where TEntity : class, IEntity
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly DbSet<TEntity> _entityDbSet;

    /// <summary>
    /// The default constructor
    /// </summary>
    /// <param name="applicationDbContext"></param>
    public AsyncCrudEntityFrameworkRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
        _entityDbSet = _applicationDbContext.Set<TEntity>();
    }
        
    /// <summary>
    /// Adds an entity
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<TEntity> Add(
        TEntity entity, 
        CancellationToken cancellationToken = default)
    {
        _entityDbSet.Add(entity);
        return Task.FromResult(entity);
    }

    /// <summary>
    /// Updates an entity
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<TEntity> Update(
        TEntity entity, 
        CancellationToken cancellationToken = default)
    {
        _entityDbSet.Update(entity);
        return Task.FromResult(entity);
    }

    /// <summary>
    /// Deletes an entity
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<bool> Delete(
        TEntity entity, 
        CancellationToken cancellationToken = default)
    {
        _entityDbSet.Remove(entity);
        return Task.FromResult(true);
    }

    /// <summary>
    /// Gets first item of entity
    /// </summary>
    /// <param name="whereExpression"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<TEntity?> GetFirst(
        Expression<Func<TEntity, bool>> whereExpression, 
        CancellationToken cancellationToken = default)
    {
        return await _entityDbSet.FirstOrDefaultAsync(
            whereExpression, 
            cancellationToken);
    }

    /// <summary>
    /// Get count of occurrence of items
    /// </summary>
    /// <param name="whereExpression"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<int> GetCount(
        Expression<Func<TEntity, bool>> whereExpression, 
        CancellationToken cancellationToken = default)
    {
        return await _entityDbSet.CountAsync(
            whereExpression, 
            cancellationToken);
    }

    /// <summary>
    /// Gets a list of TEntity
    /// </summary>
    /// <param name="whereExpression"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IReadOnlyList<TEntity>> GetList(
        Expression<Func<TEntity, bool>>? whereExpression = null, 
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> queryable = _entityDbSet;
            
        if (whereExpression is not null)
            queryable = _entityDbSet.Where(whereExpression);
            
        return await queryable.ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Gets a list of <see cref="TResult"/> with running query
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="TResult"></typeparam>
    /// <returns></returns>
    public async Task<IReadOnlyList<TResult>> GetList<TResult>(
        string query, 
        CancellationToken cancellationToken = default) where TResult : class
    {
        var queryable = _applicationDbContext.Database.SqlQuery<TResult>($"{query}");
        
        return await queryable.ToListAsync(cancellationToken);
    }
}
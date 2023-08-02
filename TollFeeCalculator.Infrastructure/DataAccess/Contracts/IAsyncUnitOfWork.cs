namespace TollFeeCalculator.Infrastructure.DataAccess.Contracts;

public interface IAsyncUnitOfWork 
    : IDisposable
{
    /// <summary>
    /// Save changes for all entities to database
    /// </summary>
    /// <returns></returns>
    Task<int> SaveChanges(CancellationToken cancellationToken);
}
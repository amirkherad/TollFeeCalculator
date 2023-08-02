using TollFeeCalculator.Infrastructure.DataAccess.Contracts;

namespace TollFeeCalculator.Infrastructure.DataAccess.Repositories.Shared;

public class AsyncUnitOfWork 
    : IAsyncUnitOfWork
{
    private readonly ApplicationDbContext _applicationDbContext;
    private bool _disposed;

    public AsyncUnitOfWork(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public Task<int> SaveChanges(CancellationToken cancellationToken)
    {
        return _applicationDbContext.SaveChangesAsync(cancellationToken);
    }
    
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed is false)
        {
            if (disposing)
            {
                _applicationDbContext.Dispose();
            }
        }
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
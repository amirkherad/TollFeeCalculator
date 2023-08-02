using Microsoft.Extensions.DependencyInjection;
using TollFeeCalculator.Infrastructure.DataAccess.Contracts;
using TollFeeCalculator.Infrastructure.DataAccess.Repositories.Shared;

namespace TollFeeCalculator.Infrastructure.Shared.Extensions.DependencyInjection;

public static class UnitOfWorkExtensions
{
    public static IServiceCollection AddUnitOfWork(this IServiceCollection service)
    {
        return service.AddScoped<IAsyncUnitOfWork, AsyncUnitOfWork>();
    }
}
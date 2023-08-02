using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TollFeeCalculator.Infrastructure.DataAccess;
using TollFeeCalculator.Infrastructure.DataAccess.ConnectionStrings;
using TollFeeCalculator.Infrastructure.DataAccess.ConnectionStrings.Contracts;
using TollFeeCalculator.Infrastructure.DataAccess.Contracts;
using TollFeeCalculator.Infrastructure.DataAccess.Repositories.Shared;

namespace TollFeeCalculator.Infrastructure.Shared.Extensions.DependencyInjection;

public static class DbContextExtensions
{
    public static IServiceCollection AddDbContext(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
        {
            var sqlServerConnectionString = serviceProvider.GetRequiredService<IConnectionString<TollFeeConnectionStringFactory>>();
            options.UseSqlServer(sqlServerConnectionString.Get());
        });

        serviceCollection.AddScoped(typeof(IAsyncCrudRepository<>), typeof(AsyncCrudEntityFrameworkRepository<>));

        return serviceCollection;
    }
}
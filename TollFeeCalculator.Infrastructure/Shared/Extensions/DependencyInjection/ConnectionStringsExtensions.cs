using Microsoft.Extensions.DependencyInjection;
using TollFeeCalculator.Infrastructure.DataAccess.ConnectionStrings;
using TollFeeCalculator.Infrastructure.DataAccess.ConnectionStrings.Contracts;

namespace TollFeeCalculator.Infrastructure.Shared.Extensions.DependencyInjection;

public static class ConnectionStringsExtensions
{
    public static IServiceCollection AddConnectionStrings(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSqlServerConnectionString();

        return serviceCollection;
    }

    private static IServiceCollection AddSqlServerConnectionString(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IConnectionString<TollFeeConnectionStringFactory>, TollFeeConnectionStringFactory>();
        serviceCollection.AddSingleton<IDbConnectionFactory<TollFeeConnectionFactory, TollFeeConnectionStringFactory>, TollFeeConnectionFactory>();
        return serviceCollection;
    }
}
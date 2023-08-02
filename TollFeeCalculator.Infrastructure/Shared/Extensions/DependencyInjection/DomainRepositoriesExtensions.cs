using Microsoft.Extensions.DependencyInjection;

namespace TollFeeCalculator.Infrastructure.Shared.Extensions.DependencyInjection;

public static class DomainRepositoriesExtensions
{
    public static IServiceCollection AddDomainRepositories(this IServiceCollection serviceCollection)
    {
        return serviceCollection;
    }
}
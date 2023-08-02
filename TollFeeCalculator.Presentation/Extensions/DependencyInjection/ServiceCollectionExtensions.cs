using TollFeeCalculator.Application.Shared.Extensions.DependencyInjection;

namespace TollFeeCalculator.Presentation.Extensions.DependencyInjection;

/// <summary>
/// Add services to Dependency Injection
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to Dependency Injection
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services
            .AddEndpointsApiExplorer()
            .AddSwaggerDocumentation()
            .AddDomainServices()
            ;
        
        return services;
    }
}
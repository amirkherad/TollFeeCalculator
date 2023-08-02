using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace TollFeeCalculator.Application.Shared.Extensions.DependencyInjection;

/// <summary>
/// The extensions of adding required services for registering autoMapper in serviceCollection
/// </summary>
public static class AutoMapperExtensions
{
    private static readonly Assembly ApplicationAssembly;

    static AutoMapperExtensions()
    {
        ApplicationAssembly = Assembly.GetAssembly(typeof(IApplicationMarker)) 
                              ?? throw new NullReferenceException(nameof(IApplicationMarker));
    }

    /// <summary>
    /// Adds the autoMapper to service collection
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        var assemblies = new List<Assembly?> { ApplicationAssembly };
        services.AddAutoMapper(assemblies, ServiceLifetime.Singleton);
        return services;
    }
}
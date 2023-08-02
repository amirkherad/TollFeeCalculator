using Microsoft.Extensions.DependencyInjection;

namespace TollFeeCalculator.Application.Shared.Extensions.DependencyInjection;

public static class DomainRuleServicesExtensions
{
    /// <summary>
    /// Adds domain rule services to service collection
    /// </summary>
    /// <param name="service"></param>
    /// <returns></returns>
    public static IServiceCollection AddDomainServices(this IServiceCollection service)
    {
        return service;
    }
}
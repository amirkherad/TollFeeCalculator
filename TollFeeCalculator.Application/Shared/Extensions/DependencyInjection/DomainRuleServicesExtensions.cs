using Microsoft.Extensions.DependencyInjection;
using TollFeeCalculator.Application.Handlers.Cities.RulesImplementations;
using TollFeeCalculator.Application.Handlers.Provinces.RulesImplementations;
using TollFeeCalculator.Domain.Entities.Cities.Rules.Contracts;
using TollFeeCalculator.Domain.Entities.Provinces.Rules.Contracts;

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
        return service.AddCustomerRulesServicesChecker();
    }

    private static IServiceCollection AddCustomerRulesServicesChecker(this IServiceCollection service)
    {
        return service
            .AddScoped<IProvinceNameUniquenessChecker, ProvinceNameUniquenessChecker>()
            .AddScoped<IProvinceExistingChecker, ProvinceExistingChecker>()
            .AddScoped<ICityNameUniquenessChecker, CityNameUniquenessChecker>();
    }
}
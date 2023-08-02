using Microsoft.Extensions.DependencyInjection;
using TollFeeCalculator.Domain.Entities.Cities;
using TollFeeCalculator.Domain.Entities.Provinces;
using TollFeeCalculator.Domain.Entities.SingleChargeRules;
using TollFeeCalculator.Domain.Entities.TimeAmounts;
using TollFeeCalculator.Domain.Entities.VehicleTypes;
using TollFeeCalculator.Infrastructure.DataAccess.Repositories;

namespace TollFeeCalculator.Infrastructure.Shared.Extensions.DependencyInjection;

public static class DomainRepositoriesExtensions
{
    public static IServiceCollection AddDomainRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddScoped<IProvinceRepository, ProvinceRepository>()
            .AddScoped<ICityRepository, CityRepository>()
            .AddScoped<ISingleChargeRuleRepository, SingleChargeRuleRepository>()
            .AddScoped<ITimeAmountRepository, TimeAmountRepository>()
            .AddScoped<IVehicleTypeRepository, VehicleTypeRepository>();
        
        return serviceCollection;
    }
}
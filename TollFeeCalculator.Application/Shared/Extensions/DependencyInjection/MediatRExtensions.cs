using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using TollFeeCalculator.Application.Handlers.Cities.Add;
using TollFeeCalculator.Application.Handlers.Cities.Get;
using TollFeeCalculator.Application.Handlers.Provinces.Add;
using TollFeeCalculator.Application.Handlers.Provinces.Get;
using TollFeeCalculator.Application.Handlers.SingleChargeRules.Add;
using TollFeeCalculator.Application.Handlers.SingleChargeRules.Get;
using TollFeeCalculator.Application.Handlers.TimeAmounts.Add;
using TollFeeCalculator.Application.Handlers.TimeAmounts.Get;
using TollFeeCalculator.Application.Handlers.TollFee.Get;
using TollFeeCalculator.Application.Handlers.VehicleTypes.Add;
using TollFeeCalculator.Application.Handlers.VehicleTypes.Get;
using TollFeeCalculator.Application.Shared.Contracts;

namespace TollFeeCalculator.Application.Shared.Extensions.DependencyInjection;

public static class MediatRExtensions
{
    private static readonly Assembly ApplicationAssembly;

    static MediatRExtensions()
    {
        ApplicationAssembly = Assembly.GetAssembly(typeof(IApplicationMarker)) 
                              ?? throw new NullReferenceException(nameof(IApplicationMarker));
    }

    /// <summary>
    /// Adds the mediatR and defined related handlers to service collection
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddMediatR(this IServiceCollection services)
    {
        // TODO: Use reflection for register handlers
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(ApplicationAssembly);
            
            configuration.AddBehavior(typeof(ICommandHandler<AddVehicleTypeCommand, AddVehicleTypeCommandResult>), typeof(AddVehicleTypeCommandHandler));
            configuration.AddBehavior(typeof(IQueryHandler<GetVehicleTypesQuery, IReadOnlyList<GetVehicleTypesQueryResult>>), typeof(GetVehicleTypesQueryHandler));
            
            configuration.AddBehavior(typeof(ICommandHandler<AddCityCommand, AddCityCommandResult>), typeof(AddCityCommandHandler));
            configuration.AddBehavior(typeof(IQueryHandler<GetCitiesQuery, IReadOnlyList<GetCitiesQueryResult>?>), typeof(GetCitiesQueryHandler));
            
            configuration.AddBehavior(typeof(ICommandHandler<AddProvinceCommand, AddProvinceCommandResult>), typeof(AddProvinceCommandHandler));
            configuration.AddBehavior(typeof(IQueryHandler<GetProvincesQuery, IReadOnlyList<GetProvincesQueryResult>?>), typeof(GetProvincesQueryHandler));
            
            configuration.AddBehavior(typeof(ICommandHandler<AddSingleChargeRuleCommand, AddSingleChargeRuleCommandResult>), typeof(AddSingleChargeRuleCommandHandler));
            configuration.AddBehavior(typeof(IQueryHandler<GetSingleChargeRuleQuery, GetSingleChargeRuleQueryResult>), typeof(GetSingleChargeRuleQueryHandler));
            configuration.AddBehavior(typeof(IQueryHandler<GetSingleChargeRulesQuery, IReadOnlyList<GetSingleChargeRuleQueryResult>>), typeof(GetSingleChargeRulesQueryHandler));
            
            configuration.AddBehavior(typeof(ICommandHandler<AddTimeAmountCommand, AddTimeAmountCommandResult>), typeof(AddTimeAmountCommandHandler));
            configuration.AddBehavior(typeof(IQueryHandler<GetTimeAmountQuery, GetTimeAmountQueryResult>), typeof(GetTimeAmountQueryHandler));
            configuration.AddBehavior(typeof(IQueryHandler<GetTimeAmountsQuery, IReadOnlyList<GetTimeAmountQueryResult>>), typeof(GetTimeAmountsQueryHandler));
            
            configuration.AddBehavior(typeof(IQueryHandler<GetVehicleTypeTollFeeQuery, GetVehicleTypeTollFeeQueryResult>), typeof(GetVehicleTypeTollFeeQueryHandler));
        });
        
        return services;
    }
}
using AutoMapper;
using TollFeeCalculator.Application.Handlers.Cities.Add;
using TollFeeCalculator.Application.Handlers.Cities.Get;
using TollFeeCalculator.Application.Handlers.Provinces.Add;
using TollFeeCalculator.Application.Handlers.Provinces.Get;
using TollFeeCalculator.Application.Handlers.SingleChargeRules.Add;
using TollFeeCalculator.Application.Handlers.TimeAmounts.Add;
using TollFeeCalculator.Application.Handlers.VehicleTypes.Add;
using TollFeeCalculator.Application.Handlers.VehicleTypes.Get;
using TollFeeCalculator.Domain.Entities.Cities;
using TollFeeCalculator.Domain.Entities.Provinces;
using TollFeeCalculator.Domain.Entities.VehicleTypes;

namespace TollFeeCalculator.Application.Shared.Configurations;

/// <summary>
/// Introduction of objects that are going to convert to each other with autoMapper
/// </summary>
public class AutoMapperProfiles 
    : Profile
{
    public AutoMapperProfiles()
    {
        // Add source and destination models and dtos to convert

        ProvinceMappings();
        CityMappings();
        SingleChargeRuleMapping();
        TimeAmountMapping();
        VehicleTypeMapping();
    }

    private void ProvinceMappings()
    {
        CreateMap<AddProvinceCommand, Province>();
        CreateMap<Province, AddProvinceCommandResult>();
        CreateMap<Province, GetProvincesQueryResult>();
    }

    private void CityMappings()
    {
        CreateMap<AddCityCommand, City>();
        CreateMap<City, AddCityCommandResult>();
        CreateMap<City, GetCitiesQueryResult>();
    }

    private void SingleChargeRuleMapping()
    {
        CreateMap<AddSingleChargeRuleCommand, AddSingleChargeRuleCommandResult>();
    }
    
    private void TimeAmountMapping()
    {
        CreateMap<AddTimeAmountCommand, AddTimeAmountCommandResult>();
    }
    
    private void VehicleTypeMapping()
    {
        CreateMap<VehicleType, AddVehicleTypeCommandResult>();
        CreateMap<VehicleType, GetVehicleTypesQueryResult>();
    }
}
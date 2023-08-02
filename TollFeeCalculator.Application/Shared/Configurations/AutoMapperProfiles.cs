using AutoMapper;

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
    }
}
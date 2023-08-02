using TollFeeCalculator.Domain.Contracts;
using TollFeeCalculator.Domain.Shared.BaseClasses;

namespace TollFeeCalculator.Domain.Entities.SingleChargeRules;

public partial class SingleChargeRule     
    : BaseEntity, IEntity
{
    public SingleChargeRule()
    {
        City = null!;
    }
    
    public SingleChargeRule(
        int cityId, 
        int? vehicleTypeId, 
        TimeSpan periodOfTime)
    {
        // TODO: Validate
        
        CityId = cityId;
        PeriodOfTime = periodOfTime;
        VehicleTypeId = vehicleTypeId;
        
        // Raise event
    }
}
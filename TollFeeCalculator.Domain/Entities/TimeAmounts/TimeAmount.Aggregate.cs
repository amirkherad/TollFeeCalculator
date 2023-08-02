using TollFeeCalculator.Domain.Contracts;
using TollFeeCalculator.Domain.Shared.BaseClasses;

namespace TollFeeCalculator.Domain.Entities.TimeAmounts;

public partial class TimeAmount
    : BaseEntity, IEntity
{
    public TimeAmount()
    {
        City = null!;
    }

    public TimeAmount(
        int cityId, 
        int? vehicleTypeId, 
        TimeSpan from, 
        TimeSpan to,
        long amount)
    {
        // TODO: validate
        CityId = cityId;
        
        // TODO: validate
        VehicleTypeId = vehicleTypeId;
        
        // TODO: validate
        From = from;

        // TODO: validate
        To = to;

        // TODO: validate
        Amount = amount;
        
        City = null!;
    }
}
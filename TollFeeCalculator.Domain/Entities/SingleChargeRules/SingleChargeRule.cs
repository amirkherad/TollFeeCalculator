using TollFeeCalculator.Domain.Entities.Cities;
using VehicleType = TollFeeCalculator.Domain.Entities.VehicleTypes.VehicleType;

namespace TollFeeCalculator.Domain.Entities.SingleChargeRules;

public partial class SingleChargeRule
{
    public int CityId { get; private set; }
    public int? VehicleTypeId { get; private set; }
    public TimeSpan PeriodOfTime { get; private set; }
    
    public virtual City? City { get; private set; }
    public virtual VehicleType? VehicleType { get; private set; }
}
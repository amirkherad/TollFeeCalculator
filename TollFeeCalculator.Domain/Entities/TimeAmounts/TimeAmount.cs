using TollFeeCalculator.Domain.Entities.Cities;
using VehicleType = TollFeeCalculator.Domain.Entities.VehicleTypes.VehicleType;

namespace TollFeeCalculator.Domain.Entities.TimeAmounts;

public partial class TimeAmount 
{
    public int CityId { get; private set; }
    public int? VehicleTypeId { get; private set; }
    public TimeSpan From { get; private set; }
    public TimeSpan To { get; private set; }
    public long Amount { get; private set; }
    
    public virtual City City { get; private set; }
    public VehicleType? VehicleType { get; private set; }
}
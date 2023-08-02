using TollFeeCalculator.Domain.Entities.SingleChargeRules;
using TollFeeCalculator.Domain.Entities.TimeAmounts;

namespace TollFeeCalculator.Domain.Entities.VehicleTypes;

public partial class VehicleType 
{
    public string Name { get; private set; }
    
    public virtual ICollection<SingleChargeRule> SingleChargeRules { get; private set; }
    public virtual ICollection<TimeAmount> TimeAmounts { get; private set; }
}
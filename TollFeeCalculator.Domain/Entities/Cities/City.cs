using Province = TollFeeCalculator.Domain.Entities.Provinces.Province;
using SingleChargeRule = TollFeeCalculator.Domain.Entities.SingleChargeRules.SingleChargeRule;
using TimeAmount = TollFeeCalculator.Domain.Entities.TimeAmounts.TimeAmount;

namespace TollFeeCalculator.Domain.Entities.Cities;

public partial class City 
{
    public int ProvinceId { get; private set; }
    public string Name { get; private set; }
    
    public virtual Province? Province { get; private set; }
    public virtual ICollection<SingleChargeRule> SingleChargeRules { get; private set; }
    public virtual ICollection<TimeAmount> TimeAmounts { get; private set; }
}
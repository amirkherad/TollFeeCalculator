using TollFeeCalculator.Application.Shared.Contracts;

namespace TollFeeCalculator.Application.Handlers.SingleChargeRules.Add;

public class AddSingleChargeRuleCommand 
    : ICommand<AddSingleChargeRuleCommandResult>
{
    public int CityId { get; set; }
    public List<AddSingleChargeRuleDto> SingleChargeRules { get; set; }
}
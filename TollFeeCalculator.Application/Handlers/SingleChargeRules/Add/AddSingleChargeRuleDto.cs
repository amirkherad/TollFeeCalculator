namespace TollFeeCalculator.Application.Handlers.SingleChargeRules.Add;

public class AddSingleChargeRuleDto
{
    public int? VehicleTypeId { get; set; }
    public TimeOnly PeriodOfTime { get; set; }
}
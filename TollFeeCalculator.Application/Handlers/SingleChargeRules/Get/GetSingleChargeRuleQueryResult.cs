namespace TollFeeCalculator.Application.Handlers.SingleChargeRules.Get;

public class GetSingleChargeRuleQueryResult
{
    public int? VehicleTypeId { get; private set; }
    public TimeSpan PeriodOfTime { get; private set; }

    public GetSingleChargeRuleQueryResult(
        int? vehicleTypeId, 
        TimeSpan periodOfTime)
    {
        VehicleTypeId = vehicleTypeId;
        PeriodOfTime = periodOfTime;
    }
}
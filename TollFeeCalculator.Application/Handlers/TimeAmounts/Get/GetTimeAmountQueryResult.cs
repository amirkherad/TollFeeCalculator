namespace TollFeeCalculator.Application.Handlers.TimeAmounts.Get;

public class GetTimeAmountQueryResult
{
    public int? VehicleTypeId { get; private set; }
    public TimeSpan From { get; private set; }
    public TimeSpan To { get; private set; }
    public long Amount { get; private set; }

    public GetTimeAmountQueryResult(
        int? vehicleTypeId, 
        TimeSpan from, 
        TimeSpan to, 
        long amount)
    {
        VehicleTypeId = vehicleTypeId;
        From = from;
        To = to;
        Amount = amount;
    }
}
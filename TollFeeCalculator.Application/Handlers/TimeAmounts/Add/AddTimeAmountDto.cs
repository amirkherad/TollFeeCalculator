namespace TollFeeCalculator.Application.Handlers.TimeAmounts.Add;

public class AddTimeAmountDto 
{
    public int? VehicleTypeId { get; set; }
    public TimeOnly From { get; set; }
    public TimeOnly To { get; set; }
    public long Amout { get; set; }
}
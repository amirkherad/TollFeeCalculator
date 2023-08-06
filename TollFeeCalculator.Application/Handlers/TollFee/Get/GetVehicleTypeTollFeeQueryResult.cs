namespace TollFeeCalculator.Application.Handlers.TollFee.Get;

public class GetVehicleTypeTollFeeQueryResult
{
    public long TollFee { get; private set; }

    public GetVehicleTypeTollFeeQueryResult(long tollFee)
    {
        TollFee = tollFee;
    }
}
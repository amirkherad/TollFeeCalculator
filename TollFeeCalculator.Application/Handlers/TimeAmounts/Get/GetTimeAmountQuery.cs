using TollFeeCalculator.Application.Shared.Contracts;

namespace TollFeeCalculator.Application.Handlers.TimeAmounts.Get;

public class GetTimeAmountQuery 
    : IQuery<GetTimeAmountQueryResult>
{
    public int CityId { get; private set; }
    public int VehicleTypeId { get; private set; }

    public GetTimeAmountQuery(
        int cityId, 
        int vehicleTypeId)
    {
        CityId = cityId;
        VehicleTypeId = vehicleTypeId;
    }
}
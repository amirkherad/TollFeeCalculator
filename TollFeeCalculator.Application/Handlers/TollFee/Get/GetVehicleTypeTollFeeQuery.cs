using TollFeeCalculator.Application.Shared.Contracts;

namespace TollFeeCalculator.Application.Handlers.TollFee.Get;

public class GetVehicleTypeTollFeeQuery 
    : IQuery<GetVehicleTypeTollFeeQueryResult>
{
    public int CityId { get; private set; }
    public int VehicleTypeId { get; private set; }
    public DateTime[] DateTimes { get; private set; }
    
    public GetVehicleTypeTollFeeQuery(
        int cityId, 
        int vehicleTypeId, 
        DateTime[] dateTimes)
    {
        CityId = cityId;
        VehicleTypeId = vehicleTypeId;
        
        // Remove duplicated items
        DateTimes = dateTimes
            .GroupBy(x => x)
            .Select(x => x.Key)
            .ToArray();
    }
}
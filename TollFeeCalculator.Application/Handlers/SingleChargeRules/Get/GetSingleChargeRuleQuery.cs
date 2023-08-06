using TollFeeCalculator.Application.Shared.Contracts;

namespace TollFeeCalculator.Application.Handlers.SingleChargeRules.Get;

public class GetSingleChargeRuleQuery 
    : IQuery<GetSingleChargeRuleQueryResult>
{
    public int CityId { get; private set; }
    public int VehicleTypeId { get; private set; }

    public GetSingleChargeRuleQuery(
        int cityId, 
        int vehicleTypeId)
    {
        CityId = cityId;
        VehicleTypeId = vehicleTypeId;
    }
}
using TollFeeCalculator.Application.Shared.Contracts;

namespace TollFeeCalculator.Application.Handlers.SingleChargeRules.Get;

public class GetSingleChargeRulesQuery 
    : IQuery<IReadOnlyList<GetSingleChargeRuleQueryResult>>
{
    public int CityId { get; private set; }

    public GetSingleChargeRulesQuery(int cityId)
    {
        CityId = cityId;
    }
}
using TollFeeCalculator.Application.Shared.Contracts;

namespace TollFeeCalculator.Application.Handlers.TimeAmounts.Get;

public class GetTimeAmountsQuery 
    : IQuery<IReadOnlyList<GetTimeAmountQueryResult>>
{
    public int CityId { get; private set; }

    public GetTimeAmountsQuery(int cityId)
    {
        CityId = cityId;
    }
}
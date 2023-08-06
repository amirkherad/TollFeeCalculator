using TollFeeCalculator.Application.Shared.Contracts;

namespace TollFeeCalculator.Application.Handlers.Cities.Get;

public class GetCitiesQuery 
    : IQuery<IReadOnlyList<GetCitiesQueryResult>>
{
    public int ProvinceId { get; private set; }
    
    public GetCitiesQuery(int provinceId)
    {
        ProvinceId = provinceId;
    }
}
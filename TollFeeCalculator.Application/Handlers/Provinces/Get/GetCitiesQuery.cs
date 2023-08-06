using TollFeeCalculator.Application.Shared.Contracts;

namespace TollFeeCalculator.Application.Handlers.Provinces.Get;

public class GetProvincesQuery 
    : IQuery<IReadOnlyList<GetProvincesQueryResult>>
{
}
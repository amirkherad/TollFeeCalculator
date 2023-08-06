using TollFeeCalculator.Application.Shared.Contracts;
using TollFeeCalculator.Domain.Entities.TimeAmounts;

namespace TollFeeCalculator.Application.Handlers.TimeAmounts.Get;

public class GetTimeAmountQueryHandler
    : IQueryHandler<GetTimeAmountQuery, GetTimeAmountQueryResult>
{
    private readonly ITimeAmountRepository _timeAmountRepository;

    public GetTimeAmountQueryHandler(ITimeAmountRepository timeAmountRepository)
    {
        _timeAmountRepository = timeAmountRepository;
    }

    public async Task<GetTimeAmountQueryResult?> Handle(
        GetTimeAmountQuery request,
        CancellationToken cancellationToken)
    {
        var timeAmounts = await _timeAmountRepository.Get(
            request.CityId, 
            request.VehicleTypeId, 
            cancellationToken);

        return timeAmounts
            .GroupBy(x => new
            {
                x.VehicleTypeId,
                x.From,
                x.To
            })
            .Select(x => new
            {
                TimeAmount = x
                    .OrderByDescending(i => i.CreatedOn)
                    .First()
            })
            .Select(x => new GetTimeAmountQueryResult(x.TimeAmount.VehicleTypeId, x.TimeAmount.From, x.TimeAmount.To, x.TimeAmount.Amount))
            .FirstOrDefault();
    }
}
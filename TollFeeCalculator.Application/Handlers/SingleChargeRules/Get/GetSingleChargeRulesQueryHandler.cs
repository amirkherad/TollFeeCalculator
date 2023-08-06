using TollFeeCalculator.Application.Shared.Contracts;
using TollFeeCalculator.Domain.Entities.SingleChargeRules;

namespace TollFeeCalculator.Application.Handlers.SingleChargeRules.Get;

public class GetSingleChargeRulesQueryHandler 
    : IQueryHandler<GetSingleChargeRulesQuery, IReadOnlyList<GetSingleChargeRuleQueryResult>>
{
    private readonly ISingleChargeRuleRepository _singleChargeRuleRepository;

    public GetSingleChargeRulesQueryHandler(ISingleChargeRuleRepository singleChargeRuleRepository)
    {
        _singleChargeRuleRepository = singleChargeRuleRepository;
    }

    public async Task<IReadOnlyList<GetSingleChargeRuleQueryResult>?> Handle(
        GetSingleChargeRulesQuery request, 
        CancellationToken cancellationToken)
    {
        var timeAmounts = await _singleChargeRuleRepository.Get(
            request.CityId, 
            cancellationToken);
        
        return timeAmounts
            .GroupBy(x => new
            {
                x.VehicleTypeId,
            })
            .Select(x => new
            {
                TimeAmount = x
                    .OrderByDescending(i => i.CreatedOn)
                    .First()
            })
            .Select(x => new GetSingleChargeRuleQueryResult(x.TimeAmount.VehicleTypeId, x.TimeAmount.PeriodOfTime))
            .ToList();
    }
}
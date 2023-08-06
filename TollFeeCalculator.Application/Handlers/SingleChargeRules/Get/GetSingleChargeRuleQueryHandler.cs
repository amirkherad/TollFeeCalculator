using TollFeeCalculator.Application.Shared.Contracts;
using TollFeeCalculator.Domain.Entities.SingleChargeRules;

namespace TollFeeCalculator.Application.Handlers.SingleChargeRules.Get;

public class GetSingleChargeRuleQueryHandler 
    : IQueryHandler<GetSingleChargeRuleQuery, GetSingleChargeRuleQueryResult>
{
    private readonly ISingleChargeRuleRepository _singleChargeRuleRepository;

    public GetSingleChargeRuleQueryHandler(ISingleChargeRuleRepository singleChargeRuleRepository)
    {
        _singleChargeRuleRepository = singleChargeRuleRepository;
    }

    public async Task<GetSingleChargeRuleQueryResult?> Handle(
        GetSingleChargeRuleQuery request, 
        CancellationToken cancellationToken)
    {
        var timeAmounts = await _singleChargeRuleRepository.Get(
            request.CityId, 
            request.VehicleTypeId,
            cancellationToken);
            
        return timeAmounts
            .OrderByDescending(x => x.VehicleTypeId)
            .ThenByDescending(x => x.CreatedOn)
            .Select(x => new GetSingleChargeRuleQueryResult(x.VehicleTypeId, x.PeriodOfTime))
            .FirstOrDefault();
    }
}
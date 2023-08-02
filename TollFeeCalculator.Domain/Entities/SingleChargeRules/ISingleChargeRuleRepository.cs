namespace TollFeeCalculator.Domain.Entities.SingleChargeRules;

/// <summary>
/// The repository for database operations of singleChargeRule
/// </summary>
public interface ISingleChargeRuleRepository 
{
    Task<SingleChargeRule> Add(
        SingleChargeRule singleChargeRule, 
        CancellationToken cancellationToken);

    Task<IReadOnlyList<SingleChargeRule>> Get(
        int cityId,
        CancellationToken cancellationToken);
    
    Task<IReadOnlyList<SingleChargeRule>> Get(
        int cityId,
        int vehicleTypeId,
        CancellationToken cancellationToken);
}
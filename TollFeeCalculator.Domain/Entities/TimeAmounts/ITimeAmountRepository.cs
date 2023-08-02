namespace TollFeeCalculator.Domain.Entities.TimeAmounts;

/// <summary>
/// The repository for database operations of timeAmount
/// </summary>
public interface ITimeAmountRepository 
{
    Task<TimeAmount> Add(
        TimeAmount timeAmount, 
        CancellationToken cancellationToken);

    Task<IReadOnlyList<TimeAmount>> Get(
        int cityId,
        int vehicleTypeId,
        CancellationToken cancellationToken);

    Task<IReadOnlyList<TimeAmount>> Get(
        int cityId,
        CancellationToken cancellationToken);
}
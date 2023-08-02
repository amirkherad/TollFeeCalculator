namespace TollFeeCalculator.Domain.Entities.Provinces;

/// <summary>
/// The repository for database operations of province
/// </summary>
public interface IProvinceRepository 
{
    Task<Province> Add(
        Province province, 
        CancellationToken cancellationToken);

    Task<IReadOnlyList<Province>> Get(CancellationToken cancellationToken);
}
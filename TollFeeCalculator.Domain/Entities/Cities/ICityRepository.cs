namespace TollFeeCalculator.Domain.Entities.Cities;

/// <summary>
/// The repository for database operations of city
/// </summary>
public interface ICityRepository 
{
    Task<City> Add(
        City city, 
        CancellationToken cancellationToken);

    Task<IReadOnlyList<City>> Get(
        int provinceId, 
        CancellationToken cancellationToken);
}
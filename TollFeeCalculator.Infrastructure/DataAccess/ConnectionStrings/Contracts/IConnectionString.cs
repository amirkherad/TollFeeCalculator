namespace TollFeeCalculator.Infrastructure.DataAccess.ConnectionStrings.Contracts;

/// <summary>
/// Abstraction of ConnectionString
/// </summary>
/// <typeparam name="TConnectionStringFactory"></typeparam>
public interface IConnectionString<TConnectionStringFactory>
{
    /// <summary>
    /// Get a connectionString for generic type
    /// </summary>
    /// <returns></returns>
    string Get();
}
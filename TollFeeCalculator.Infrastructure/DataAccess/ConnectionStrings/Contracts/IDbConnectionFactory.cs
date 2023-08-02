using System.Data;

namespace TollFeeCalculator.Infrastructure.DataAccess.ConnectionStrings.Contracts;

/// <summary>
/// Creator of <see cref="IDbConnection"/>
/// </summary>
/// <typeparam name="TConnectionFactory"></typeparam>
/// <typeparam name="TConnectionStringFactory"></typeparam>
public interface IDbConnectionFactory<TConnectionFactory, TConnectionStringFactory> 
{
    /// <summary>
    /// Get an <see cref="IDbConnection"/> for connecting to generic databaseType 
    /// </summary>
    /// <returns></returns>
    IDbConnection Get();
}
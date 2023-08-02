using System.Data;
using Microsoft.Data.SqlClient;
using TollFeeCalculator.Infrastructure.DataAccess.ConnectionStrings.Contracts;

namespace TollFeeCalculator.Infrastructure.DataAccess.ConnectionStrings;

/// <summary>
/// Creator of connection to sqlServer
/// </summary>
/// <typeparam name="TSqlServerConnectionFactory"></typeparam>
/// <typeparam name="TConnectionStringFactory"></typeparam>
public class BaseSqlServerConnectionFactory<TSqlServerConnectionFactory, TConnectionStringFactory> 
    : IDbConnectionFactory<TSqlServerConnectionFactory, TConnectionStringFactory>
{
    private readonly IConnectionString<TConnectionStringFactory> _connectionString;

    /// <summary>
    /// The default constructor
    /// </summary>
    /// <param name="connectionString"></param>
    protected BaseSqlServerConnectionFactory(IConnectionString<TConnectionStringFactory> connectionString)
    {
        _connectionString = connectionString;
    }

    /// <summary>
    /// Get an <see cref="IDbConnection"/> for connecting to sqlServer 
    /// </summary>
    /// <returns></returns>
    public IDbConnection Get() => new SqlConnection(_connectionString.Get());
}
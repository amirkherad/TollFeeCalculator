using Microsoft.Extensions.Configuration;
using TollFeeCalculator.Infrastructure.DataAccess.ConnectionStrings.Contracts;

namespace TollFeeCalculator.Infrastructure.DataAccess.ConnectionStrings;

/// <summary>
/// The base class of fetching connectionStrings of sql server databases that have used in application
/// A class for each sql server database inherits from this base class 
/// </summary>
/// <typeparam name="TSqlServerConnectionStringFactory"></typeparam>
public class BaseSqlServerConnectionStringFactory<TSqlServerConnectionStringFactory>
    : IConnectionString<TSqlServerConnectionStringFactory>
{
    private readonly IConfigurationRoot _configuration;
    private readonly string _serviceName;

    /// <summary>
    /// The default constructor
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="serviceName"></param>
    protected BaseSqlServerConnectionStringFactory(
        IConfigurationRoot configuration, 
        string serviceName)
    {
        _configuration = configuration;
        _serviceName = serviceName.Replace("ConnectionStringFactory", "");
    }

    /// <summary>
    /// Fetches connectionString that related to service name from appsettings.json 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public string Get()
    {
        var connectionString = _configuration.GetConnectionString(_serviceName);
        
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new Exception($"ConnectionString for {_serviceName} was not found in appsettings.");

        return connectionString;
    }
}
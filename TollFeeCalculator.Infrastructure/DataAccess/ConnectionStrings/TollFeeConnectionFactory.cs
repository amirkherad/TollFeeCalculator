using TollFeeCalculator.Infrastructure.DataAccess.ConnectionStrings.Contracts;

namespace TollFeeCalculator.Infrastructure.DataAccess.ConnectionStrings;

public class TollFeeConnectionFactory
    : BaseSqlServerConnectionFactory<TollFeeConnectionFactory, TollFeeConnectionStringFactory>
{
    /// <summary>
    /// The default constructor
    /// </summary>
    /// <param name="connectionString"></param>
    public TollFeeConnectionFactory(IConnectionString<TollFeeConnectionStringFactory> connectionString) 
        : base(connectionString)
    {
    }
}
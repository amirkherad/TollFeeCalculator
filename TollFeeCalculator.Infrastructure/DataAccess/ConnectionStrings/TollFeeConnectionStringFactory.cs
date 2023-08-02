using Microsoft.Extensions.Configuration;

namespace TollFeeCalculator.Infrastructure.DataAccess.ConnectionStrings;

public class TollFeeConnectionStringFactory
    : BaseSqlServerConnectionStringFactory<TollFeeConnectionStringFactory>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="configuration"></param>
    public TollFeeConnectionStringFactory(IConfigurationRoot configuration) 
        : base(configuration, nameof(TollFeeConnectionStringFactory))
    {
    }
}
using Dapper;
using TollFeeCalculator.Domain.Entities.Cities.Rules.Contracts;
using TollFeeCalculator.Infrastructure.DataAccess.ConnectionStrings;
using TollFeeCalculator.Infrastructure.DataAccess.ConnectionStrings.Contracts;
using TollFeeCalculator.Infrastructure.DataAccess.EntitiesConfigurations;
using TollFeeCalculator.Infrastructure.DataAccess.Enums;

namespace TollFeeCalculator.Application.Handlers.Cities.RulesImplementations;

public class ProvinceExistingChecker 
    : IProvinceExistingChecker
{
    private readonly IDbConnectionFactory<TollFeeConnectionFactory, TollFeeConnectionStringFactory> _dbConnectionFactory;

    public ProvinceExistingChecker(
        IDbConnectionFactory<TollFeeConnectionFactory, TollFeeConnectionStringFactory> dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public bool IsExists(int provinceId)
    {
        using var connection = _dbConnectionFactory.Get();
        
        var query = $"SELECT TOP 1 1 " +
                    $"FROM {DatabaseSchemaNames.Dbo.Name.ToLower()}.{DatabaseTablesName.Province} " +
                    $"WHERE [Id] = @ProvinceId";

        var parameters = new DynamicParameters();
        parameters.Add("ProvinceId", provinceId);
            
        var provinceValueForExisting = connection.QueryFirstOrDefault<int?>(query, parameters);

        return provinceValueForExisting.HasValue;
    }
}
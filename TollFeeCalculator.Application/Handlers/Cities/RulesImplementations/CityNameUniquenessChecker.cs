using Dapper;
using TollFeeCalculator.Domain.Entities.Cities.Rules.Contracts;
using TollFeeCalculator.Infrastructure.DataAccess.ConnectionStrings;
using TollFeeCalculator.Infrastructure.DataAccess.ConnectionStrings.Contracts;
using TollFeeCalculator.Infrastructure.DataAccess.EntitiesConfigurations;
using TollFeeCalculator.Infrastructure.DataAccess.Enums;

namespace TollFeeCalculator.Application.Handlers.Cities.RulesImplementations;

public class CityNameUniquenessChecker 
    : ICityNameUniquenessChecker
{
    private readonly IDbConnectionFactory<TollFeeConnectionFactory, TollFeeConnectionStringFactory> _dbConnectionFactory;

    public CityNameUniquenessChecker(
        IDbConnectionFactory<TollFeeConnectionFactory, TollFeeConnectionStringFactory> dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }
    
    public bool IsUnique(
        string name, 
        int provinceId)
    {
        using var connection = _dbConnectionFactory.Get();
        
        var query = $"SELECT TOP 1 1 " +
                    $"FROM {DatabaseSchemaNames.Dbo.Name.ToLower()}.{DatabaseTablesName.City} " +
                    $"WHERE [ProvinceId] = @ProvinceId AND [Name] = @Name";

        var parameters = new DynamicParameters();
        parameters.Add("ProvinceId", provinceId);
        parameters.Add("Name", name);
            
        var provinceValueForExisting = connection.QueryFirstOrDefault<int?>(query, parameters);

        return provinceValueForExisting.HasValue is false;
    }
}
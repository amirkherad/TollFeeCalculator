using Dapper;
using TollFeeCalculator.Domain.Entities.Provinces.Rules.Contracts;
using TollFeeCalculator.Infrastructure.DataAccess.ConnectionStrings;
using TollFeeCalculator.Infrastructure.DataAccess.ConnectionStrings.Contracts;
using TollFeeCalculator.Infrastructure.DataAccess.EntitiesConfigurations;
using TollFeeCalculator.Infrastructure.DataAccess.Enums;

namespace TollFeeCalculator.Application.Handlers.Provinces.RulesImplementations;

public class ProvinceNameUniquenessChecker 
    : IProvinceNameUniquenessChecker
{
    private readonly IDbConnectionFactory<TollFeeConnectionFactory, TollFeeConnectionStringFactory> _dbConnectionFactory;

    public ProvinceNameUniquenessChecker(
        IDbConnectionFactory<TollFeeConnectionFactory, TollFeeConnectionStringFactory> dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public bool IsUnique(string name)
    {
        using var connection = _dbConnectionFactory.Get();
        
        var query = $"SELECT TOP 1 1 " +
                    $"FROM {DatabaseSchemaNames.Dbo.Name.ToLower()}.{DatabaseTablesName.Province} " +
                    $"WHERE [Name] = @Name";

        var parameters = new DynamicParameters();
        parameters.Add("Name", name);
            
        var provinceValueForExisting = connection.QueryFirstOrDefault<int?>(query, parameters);

        return provinceValueForExisting.HasValue is false;
    }
}
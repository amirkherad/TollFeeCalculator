using Pluralize.NET.Core;

namespace TollFeeCalculator.Infrastructure.DataAccess.EntitiesConfigurations;

public class DatabaseTablesName
{
    public static readonly string City;
    public static readonly string Province;

    static DatabaseTablesName()
    {
        City = new Pluralizer().Pluralize(nameof(City));
        Province = new Pluralizer().Pluralize(nameof(Province));
    }
}
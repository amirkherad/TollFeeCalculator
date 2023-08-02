using TollFeeCalculator.Domain.Shared.BaseClasses;

namespace TollFeeCalculator.Infrastructure.DataAccess.Enums;

public class DatabaseSchemaNames 
    : Enumeration<DatabaseSchemaNames>
{
    public static readonly DatabaseSchemaNames Dbo = new(value: 1, nameof(Dbo));

    private DatabaseSchemaNames(int value, string name)
        : base(value, name)
    {
    }
}
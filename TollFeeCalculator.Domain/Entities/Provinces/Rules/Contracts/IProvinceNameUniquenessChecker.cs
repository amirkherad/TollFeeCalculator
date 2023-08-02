namespace TollFeeCalculator.Domain.Entities.Provinces.Rules.Contracts;

public interface IProvinceNameUniquenessChecker
{
    bool IsUnique(string name);
}
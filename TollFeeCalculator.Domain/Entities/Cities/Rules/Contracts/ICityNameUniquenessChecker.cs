namespace TollFeeCalculator.Domain.Entities.Cities.Rules.Contracts;

public interface ICityNameUniquenessChecker
{
    bool IsUnique(string name, int provinceId);
}
namespace TollFeeCalculator.Domain.Entities.Cities.Rules.Contracts;

public interface IProvinceExistingChecker
{
    bool IsExists(int provinceId);
}
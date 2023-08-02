using TollFeeCalculator.Domain.Contracts;
using TollFeeCalculator.Domain.Entities.Cities.Rules.Contracts;
using Province = TollFeeCalculator.Domain.Entities.Provinces.Province;

namespace TollFeeCalculator.Domain.Entities.Cities.Rules;

public class ProvinceIdShouldBeExistsRule 
    : IBusinessRule
{
    private readonly IProvinceExistingChecker _provinceExistingChecker;
    private readonly int _provinceId;

    public ProvinceIdShouldBeExistsRule(
        IProvinceExistingChecker provinceExistingChecker, 
        int provinceId)
    {
        _provinceExistingChecker = provinceExistingChecker;
        _provinceId = provinceId;
    }

    public bool IsBroken() => _provinceExistingChecker.IsExists(_provinceId) is false;

    public string Message => $"There is no {nameof(Province)} with {nameof(Province.Id)}: {_provinceId}.";
}
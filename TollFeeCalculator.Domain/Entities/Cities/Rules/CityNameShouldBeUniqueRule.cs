using TollFeeCalculator.Domain.Contracts;
using TollFeeCalculator.Domain.Entities.Cities.Rules.Contracts;
using Province = TollFeeCalculator.Domain.Entities.Provinces.Province;

namespace TollFeeCalculator.Domain.Entities.Cities.Rules;

/// <summary>
/// The business rules for name's uniqueness
/// </summary>
public class CityNameShouldBeUniqueRule 
    : IBusinessRule
{
    private readonly ICityNameUniquenessChecker _cityNameUniquenessChecker;
    private readonly string _name;
    private readonly int _provinceId;

    /// <summary>
    /// The default constructor
    /// </summary>
    /// <param name="cityNameUniquenessChecker"></param>
    /// <param name="name"></param>
    /// <param name="provinceId"></param>
    public CityNameShouldBeUniqueRule(
        ICityNameUniquenessChecker cityNameUniquenessChecker, 
        string name, 
        int provinceId)
    {
        _cityNameUniquenessChecker = cityNameUniquenessChecker;
        _name = name;
        _provinceId = provinceId;
    }

    /// <summary>
    /// Checks email is unique or not
    /// </summary>
    /// <returns></returns>
    public bool IsBroken() => _cityNameUniquenessChecker.IsUnique(_name, _provinceId) is false;

    /// <summary>
    /// The message of broken rule
    /// </summary>
    public string Message => $"{nameof(City.Name)}: {_name} in {nameof(Province)} with {nameof(Province.Id)}: {_provinceId} was already exists.";
}
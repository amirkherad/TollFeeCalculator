using TollFeeCalculator.Domain.Contracts;
using TollFeeCalculator.Domain.Entities.Provinces.Rules.Contracts;

namespace TollFeeCalculator.Domain.Entities.Provinces.Rules;

/// <summary>
/// The business rules for name's uniqueness
/// </summary>
public class ProvinceNameShouldBeUniqueRule 
    : IBusinessRule
{
    private readonly IProvinceNameUniquenessChecker _provinceNameUniquenessChecker;
    private readonly string _name;

    /// <summary>
    /// The default constructor
    /// </summary>
    /// <param name="provinceNameUniquenessChecker"></param>
    /// <param name="name"></param>
    public ProvinceNameShouldBeUniqueRule(
        IProvinceNameUniquenessChecker provinceNameUniquenessChecker, 
        string name)
    {
        _provinceNameUniquenessChecker = provinceNameUniquenessChecker;
        _name = name;
    }

    /// <summary>
    /// Checks email is unique or not
    /// </summary>
    /// <returns></returns>
    public bool IsBroken() => _provinceNameUniquenessChecker.IsUnique(_name) is false;

    /// <summary>
    /// The message of broken rule
    /// </summary>
    public string Message => $"{nameof(Province.Name)}: {_name} was already exists.";
}
using TollFeeCalculator.Domain.Contracts;
using TollFeeCalculator.Domain.Shared.BaseClasses;

namespace TollFeeCalculator.Domain.Entities.Provinces.Rules;

public class ProvinceNameShouldNotBeTooLong 
    : IBusinessRule
{
    private readonly string _name;

    /// <summary>
    /// The default constructor
    /// </summary>
    /// <param name="name"></param>
    public ProvinceNameShouldNotBeTooLong(string name)
    {
        _name = name;
    }

    /// <summary>
    /// Checks the name that its length not be too long
    /// </summary>
    /// <returns></returns>
    public bool IsBroken() => _name.Length > Constants.MaxNameLength;

    /// <summary>
    /// The message of broken rule
    /// </summary>
    public string Message => $"The {nameof(Province.Name)} can't be more than {Constants.MaxNameLength} characters.";
}
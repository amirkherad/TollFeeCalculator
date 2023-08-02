using TollFeeCalculator.Domain.Contracts;

namespace TollFeeCalculator.Domain.Entities.Cities.Rules;

public class CityNameShouldNotBeEmpty 
    : IBusinessRule
{
    private readonly string _name;

    /// <summary>
    /// The default constructor
    /// </summary>
    /// <param name="name"></param>
    public CityNameShouldNotBeEmpty(string name)
    {
        _name = name;
    }

    /// <summary>
    /// Checks the name that its length not be empty
    /// </summary>
    /// <returns></returns>
    public bool IsBroken() => string.IsNullOrWhiteSpace(_name);

    /// <summary>
    /// The message of broken rule
    /// </summary>
    public string Message => $"The {nameof(City.Name)} can't be empty.";
}
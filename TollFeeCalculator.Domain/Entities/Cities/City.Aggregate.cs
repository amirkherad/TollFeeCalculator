using TollFeeCalculator.Domain.Contracts;
using TollFeeCalculator.Domain.Entities.Cities.Events;
using TollFeeCalculator.Domain.Entities.Cities.Rules;
using TollFeeCalculator.Domain.Entities.Cities.Rules.Contracts;
using TollFeeCalculator.Domain.Shared.BaseClasses;
using SingleChargeRule = TollFeeCalculator.Domain.Entities.SingleChargeRules.SingleChargeRule;
using TimeAmount = TollFeeCalculator.Domain.Entities.TimeAmounts.TimeAmount;

namespace TollFeeCalculator.Domain.Entities.Cities;

public partial class City
    : BaseEntity, IEntity
{
    public City()
    {
        Province = null!;
        SingleChargeRules = new HashSet<SingleChargeRule>();
        TimeAmounts = new HashSet<TimeAmount>();
    }
    
    public City(
        IProvinceExistingChecker provinceExistingChecker,
        ICityNameUniquenessChecker cityNameUniquenessChecker,
        int provinceId, 
        string name)
    {
        var provinceIdShouldBeExistsRule = new ProvinceIdShouldBeExistsRule(
            provinceExistingChecker, 
            provinceId);
        CheckBusinessRule(provinceIdShouldBeExistsRule);

        ProvinceId = provinceId;

        var nameShouldNotBeEmpty = new CityNameShouldNotBeEmpty(name);
        CheckBusinessRule(nameShouldNotBeEmpty);

        var nameShouldNotBeTooLong = new CityNameShouldNotBeTooLong(name);
        CheckBusinessRule(nameShouldNotBeTooLong);

        var nameShouldBeUniqueRule = new CityNameShouldBeUniqueRule(
            cityNameUniquenessChecker,
            name,
            provinceId);
        CheckBusinessRule(nameShouldBeUniqueRule);
        
        Name = name;
        
        SingleChargeRules = new HashSet<SingleChargeRule>();
        TimeAmounts = new HashSet<TimeAmount>();

        var onCityCreatedEvent = new OnCityCreatedEvent(
            provinceId, 
            name);
        AddDomainEvent(onCityCreatedEvent);
    }
}
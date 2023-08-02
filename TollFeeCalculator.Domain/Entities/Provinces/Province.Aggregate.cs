using TollFeeCalculator.Domain.Contracts;
using TollFeeCalculator.Domain.Entities.Cities;
using TollFeeCalculator.Domain.Entities.Provinces.Events;
using TollFeeCalculator.Domain.Entities.Provinces.Rules;
using TollFeeCalculator.Domain.Entities.Provinces.Rules.Contracts;
using TollFeeCalculator.Domain.Shared.BaseClasses;

namespace TollFeeCalculator.Domain.Entities.Provinces;

public partial class Province
    : BaseEntity, IEntity
{
    public Province()
    {
        Cities = new HashSet<City>();
    }

    public Province(
        IProvinceNameUniquenessChecker provinceNameUniquenessChecker,
        string name)
    {
        var provinceNameShouldNotBeEmpty = new ProvinceNameShouldNotBeEmpty(name);
        CheckBusinessRule(provinceNameShouldNotBeEmpty);

        var provinceNameShouldNotBeTooLong = new ProvinceNameShouldNotBeTooLong(name);
        CheckBusinessRule(provinceNameShouldNotBeTooLong);

        var provinceNameShouldBeUniqueRule = new ProvinceNameShouldBeUniqueRule(
            provinceNameUniquenessChecker,
            name);
        CheckBusinessRule(provinceNameShouldBeUniqueRule);

        Name = name;
        
        Cities = new HashSet<City>();

        var onProvinceCreatedEvent = new OnProvinceCreatedEvent(name);
        AddDomainEvent(onProvinceCreatedEvent);
    }
}
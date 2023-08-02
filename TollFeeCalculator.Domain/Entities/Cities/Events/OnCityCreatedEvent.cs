using TollFeeCalculator.Domain.Contracts;

namespace TollFeeCalculator.Domain.Entities.Cities.Events;

public sealed record OnCityCreatedEvent(
    int ProvinceId,
    string Name) : IDomainEvent
{
    public int ProvinceId { get; private set; } = ProvinceId;
    public string Name { get; private set; } = Name;
}
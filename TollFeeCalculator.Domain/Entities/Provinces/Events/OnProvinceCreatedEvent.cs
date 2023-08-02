using TollFeeCalculator.Domain.Contracts;

namespace TollFeeCalculator.Domain.Entities.Provinces.Events;

public sealed record OnProvinceCreatedEvent(string Name) 
    : IDomainEvent
{
    public string Name { get; private set; } = Name;
}
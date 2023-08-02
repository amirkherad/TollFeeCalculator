using TollFeeCalculator.Domain.Contracts;
using TollFeeCalculator.Domain.Shared.Exceptions;

namespace TollFeeCalculator.Domain.Shared.BaseClasses;

public abstract class BaseEntity 
{
    public int Id { get; set; }
    
    private readonly List<IDomainEvent> _domainEvents = new();
    
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;

    public DateTimeOffset CreatedOn { get; protected init; } = DateTimeOffset.Now;

    protected void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);

    public void ClearDomainEvents() => _domainEvents.Clear();

    protected void CheckBusinessRule(IBusinessRule rule)
    {
        if (rule.IsBroken())
            throw new BusinessRuleValidationException(rule);
    }
}
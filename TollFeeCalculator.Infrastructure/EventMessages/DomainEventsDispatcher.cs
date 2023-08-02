using MediatR;
using TollFeeCalculator.Domain.Contracts;
using TollFeeCalculator.Infrastructure.EventMessages.Contracts;

namespace TollFeeCalculator.Infrastructure.EventMessages;

/// <summary>
/// The dispatcher of events 
/// </summary>
public class DomainEventsDispatcher 
    : IDomainEventsDispatcher
{
    private readonly IMediator _mediator;

    /// <summary>
    /// The default constructor
    /// </summary>
    /// <param name="mediator"></param>
    public DomainEventsDispatcher(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Dispatches the events
    /// </summary>
    /// <param name="domainEvents"></param>
    public void Dispatch(IEnumerable<IDomainEvent> domainEvents)
    {
        foreach (var domainEvent in domainEvents)
            _mediator.Publish(domainEvent);
    }
}
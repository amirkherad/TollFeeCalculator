using TollFeeCalculator.Domain.Contracts;

namespace TollFeeCalculator.Infrastructure.EventMessages.Contracts;

/// <summary>
/// 
/// </summary>
public interface IDomainEventsDispatcher
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="domainEvents"></param>
    void Dispatch(IEnumerable<IDomainEvent> domainEvents);
}
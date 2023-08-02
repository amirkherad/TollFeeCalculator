using MediatR;

namespace TollFeeCalculator.Domain.Contracts;

/// <summary>
/// Abstract interface to mark event classes 
/// </summary>
public interface IDomainEvent 
    : INotification
{
}
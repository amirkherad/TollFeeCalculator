using MediatR;

namespace TollFeeCalculator.Application.Shared.Contracts;

public interface IQuery<out TResult> 
    : IRequest<TResult?>
{
}
using MediatR;

namespace TollFeeCalculator.Application.Shared.Contracts;

public interface ICommand 
    : IRequest
{
}

public interface ICommand<out TResult> 
    : IRequest<TResult>
{
}
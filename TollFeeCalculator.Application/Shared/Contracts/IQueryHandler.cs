using MediatR;

namespace TollFeeCalculator.Application.Shared.Contracts;

public interface IQueryHandler<in TQuery, TResult> 
    : IRequestHandler<TQuery, TResult?> where TQuery : IQuery<TResult?>
{
}
using System.Linq.Expressions;
using TollFeeCalculator.Domain.Entities.TimeAmounts;
using TollFeeCalculator.Infrastructure.DataAccess.Contracts;

namespace TollFeeCalculator.Infrastructure.DataAccess.Repositories;

public class TimeAmountRepository 
    : ITimeAmountRepository
{
    private readonly IAsyncCrudRepository<TimeAmount> _asyncCrudRepository;

    public TimeAmountRepository(IAsyncCrudRepository<TimeAmount> asyncCrudRepository)
    {
        _asyncCrudRepository = asyncCrudRepository;
    }

    public Task<TimeAmount> Add(
        TimeAmount timeAmount, 
        CancellationToken cancellationToken)
    {
        return _asyncCrudRepository.Add(
            timeAmount, 
            cancellationToken);
    }

    public Task<IReadOnlyList<TimeAmount>> Get(
        int cityId, 
        int vehicleTypeId, 
        CancellationToken cancellationToken)
    {
        Expression<Func<TimeAmount, bool>> whereExpression = x => 
            x.CityId == cityId && 
            (x.VehicleTypeId == vehicleTypeId || x.VehicleTypeId == null);

        return _asyncCrudRepository.GetList(
            whereExpression, 
            cancellationToken);
    }
    
    public Task<IReadOnlyList<TimeAmount>> Get(
        int cityId, 
        CancellationToken cancellationToken)
    {
        Expression<Func<TimeAmount, bool>> whereExpression = x => x.CityId == cityId;

        return _asyncCrudRepository.GetList(
            whereExpression, 
            cancellationToken);
    }
}
using System.Linq.Expressions;
using TollFeeCalculator.Domain.Entities.SingleChargeRules;
using TollFeeCalculator.Infrastructure.DataAccess.Contracts;

namespace TollFeeCalculator.Infrastructure.DataAccess.Repositories;

public class SingleChargeRuleRepository 
    : ISingleChargeRuleRepository
{
    private readonly IAsyncCrudRepository<SingleChargeRule> _asyncCrudRepository;

    public SingleChargeRuleRepository(IAsyncCrudRepository<SingleChargeRule> asyncCrudRepository)
    {
        _asyncCrudRepository = asyncCrudRepository;
    }

    public Task<SingleChargeRule> Add(
        SingleChargeRule singleChargeRule, 
        CancellationToken cancellationToken)
    {
        return _asyncCrudRepository.Add(
            singleChargeRule, 
            cancellationToken);
    }

    public Task<IReadOnlyList<SingleChargeRule>> Get(
        int cityId, 
        CancellationToken cancellationToken)
    {
        Expression<Func<SingleChargeRule, bool>> whereExpression = x => x.CityId == cityId;

        return _asyncCrudRepository.GetList(
            whereExpression, 
            cancellationToken);
    }
    
    public Task<IReadOnlyList<SingleChargeRule>> Get(
        int cityId, 
        int vehicleTypeId, 
        CancellationToken cancellationToken)
    {
        Expression<Func<SingleChargeRule, bool>> whereExpression = x => 
            x.CityId == cityId && 
            (x.VehicleTypeId == vehicleTypeId || x.VehicleTypeId == null);

        return _asyncCrudRepository.GetList(
            whereExpression, 
            cancellationToken);
    }
}
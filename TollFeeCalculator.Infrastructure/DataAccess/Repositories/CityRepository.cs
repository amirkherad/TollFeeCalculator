using System.Linq.Expressions;
using TollFeeCalculator.Domain.Entities.Cities;
using TollFeeCalculator.Infrastructure.DataAccess.Contracts;

namespace TollFeeCalculator.Infrastructure.DataAccess.Repositories;

public class CityRepository 
    : ICityRepository
{
    private readonly IAsyncCrudRepository<City> _asyncCrudRepository;

    public CityRepository(IAsyncCrudRepository<City> asyncCrudRepository)
    {
        _asyncCrudRepository = asyncCrudRepository;
    }

    public Task<City> Add(
        City city, 
        CancellationToken cancellationToken)
    {
        return _asyncCrudRepository.Add(
            city, 
            cancellationToken);
    }

    public Task<IReadOnlyList<City>> Get(
        int provinceId, 
        CancellationToken cancellationToken)
    {
        Expression<Func<City, bool>> whereExpression = city => city.ProvinceId == provinceId;
        
        return _asyncCrudRepository.GetList(
            whereExpression,
            cancellationToken: cancellationToken);
    }
}
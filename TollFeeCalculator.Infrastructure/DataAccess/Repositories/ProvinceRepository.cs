using TollFeeCalculator.Domain.Entities.Provinces;
using TollFeeCalculator.Infrastructure.DataAccess.Contracts;

namespace TollFeeCalculator.Infrastructure.DataAccess.Repositories;

public class ProvinceRepository 
    : IProvinceRepository
{
    private readonly IAsyncCrudRepository<Province> _asyncCrudRepository;

    public ProvinceRepository(IAsyncCrudRepository<Province> asyncCrudRepository)
    {
        _asyncCrudRepository = asyncCrudRepository;
    }

    public Task<Province> Add(
        Province province, 
        CancellationToken cancellationToken)
    {
        return _asyncCrudRepository.Add(
            province, 
            cancellationToken);
    }

    public Task<IReadOnlyList<Province>> Get(CancellationToken cancellationToken)
    {
        return _asyncCrudRepository.GetList(cancellationToken: cancellationToken);
    }
}
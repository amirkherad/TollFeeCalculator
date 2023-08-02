using TollFeeCalculator.Domain.Entities.VehicleTypes;
using TollFeeCalculator.Infrastructure.DataAccess.Contracts;

namespace TollFeeCalculator.Infrastructure.DataAccess.Repositories;

public class VehicleTypeRepository 
    : IVehicleTypeRepository
{
    private readonly IAsyncCrudRepository<VehicleType> _asyncCrudRepository;

    public VehicleTypeRepository(IAsyncCrudRepository<VehicleType> asyncCrudRepository)
    {
        _asyncCrudRepository = asyncCrudRepository;
    }

    public Task<VehicleType> Add(
        VehicleType vehicleType, 
        CancellationToken cancellationToken)
    {
        return _asyncCrudRepository.Add(
            vehicleType, 
            cancellationToken);
    }

    public Task<IReadOnlyList<VehicleType>> Get(CancellationToken cancellationToken)
    {
        return _asyncCrudRepository.GetList(cancellationToken: cancellationToken);
    }
}
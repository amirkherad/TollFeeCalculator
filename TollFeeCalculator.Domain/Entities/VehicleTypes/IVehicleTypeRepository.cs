namespace TollFeeCalculator.Domain.Entities.VehicleTypes;

/// <summary>
/// The repository for database operations of <see cref="VehicleType"/>
/// </summary>
public interface IVehicleTypeRepository 
{
    Task<VehicleType> Add(
        VehicleType vehicleType, 
        CancellationToken cancellationToken);
    
    Task<IReadOnlyList<VehicleType>> Get(CancellationToken cancellationToken);
}
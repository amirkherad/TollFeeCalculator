using TollFeeCalculator.Application.Shared.Contracts;

namespace TollFeeCalculator.Application.Handlers.VehicleTypes.Get;

public class GetVehicleTypesQuery 
    : IQuery<IReadOnlyList<GetVehicleTypesQueryResult>>
{
}
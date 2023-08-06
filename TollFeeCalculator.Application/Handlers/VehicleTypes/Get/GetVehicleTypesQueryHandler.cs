using AutoMapper;
using TollFeeCalculator.Application.Shared.Contracts;
using TollFeeCalculator.Domain.Entities.VehicleTypes;

namespace TollFeeCalculator.Application.Handlers.VehicleTypes.Get;

public class GetVehicleTypesQueryHandler 
    : IQueryHandler<GetVehicleTypesQuery, IReadOnlyList<GetVehicleTypesQueryResult>>
{
    private readonly IVehicleTypeRepository _vehicleTypeRepository;
    private readonly IMapper _mapper;

    public GetVehicleTypesQueryHandler(
        IVehicleTypeRepository vehicleTypeRepository, 
        IMapper mapper)
    {
        _vehicleTypeRepository = vehicleTypeRepository;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<GetVehicleTypesQueryResult>?> Handle(
        GetVehicleTypesQuery request, 
        CancellationToken cancellationToken)
    {
        var vehicleTypes = await _vehicleTypeRepository.Get(cancellationToken);

        return _mapper.Map<IReadOnlyList<GetVehicleTypesQueryResult>>(vehicleTypes);
    }
}
using AutoMapper;
using TollFeeCalculator.Application.Shared.Contracts;
using TollFeeCalculator.Domain.Entities.VehicleTypes;
using TollFeeCalculator.Infrastructure.DataAccess.Contracts;

namespace TollFeeCalculator.Application.Handlers.VehicleTypes.Add;

public class AddVehicleTypeCommandHandler 
    : ICommandHandler<AddVehicleTypeCommand, AddVehicleTypeCommandResult?>
{
    private readonly IVehicleTypeRepository _vehicleTypeRepository;
    private readonly IAsyncUnitOfWork _asyncUnitOfWork;
    private readonly IMapper _mapper;

    public AddVehicleTypeCommandHandler(
        IVehicleTypeRepository vehicleTypeRepository, 
        IAsyncUnitOfWork asyncUnitOfWork, 
        IMapper mapper)
    {
        _vehicleTypeRepository = vehicleTypeRepository;
        _asyncUnitOfWork = asyncUnitOfWork;
        _mapper = mapper;
    }

    public async Task<AddVehicleTypeCommandResult?> Handle(
        AddVehicleTypeCommand addVehicleTypeCommand, 
        CancellationToken cancellationToken)
    {
        var vehicleType = new VehicleType(addVehicleTypeCommand.Name);

        await _vehicleTypeRepository.Add(
            vehicleType, 
            cancellationToken);

        await _asyncUnitOfWork.SaveChanges(cancellationToken);
        
        return _mapper.Map<AddVehicleTypeCommandResult>(vehicleType);
    }
}
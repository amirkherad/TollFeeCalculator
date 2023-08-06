using TollFeeCalculator.Application.Shared.Contracts;

namespace TollFeeCalculator.Application.Handlers.VehicleTypes.Add;

public class AddVehicleTypeCommand 
    : ICommand<AddVehicleTypeCommandResult>
{
    public string Name { get; set; }
}
using TollFeeCalculator.Application.Shared.Contracts;

namespace TollFeeCalculator.Application.Handlers.Provinces.Add;

public class AddProvinceCommand 
    : ICommand<AddProvinceCommandResult>
{
    public string Name { get; set; }
}
using TollFeeCalculator.Application.Shared.Contracts;

namespace TollFeeCalculator.Application.Handlers.Cities.Add;

public class AddCityCommand
    : ICommand<AddCityCommandResult>
{
    public int ProvinceId { get; private set; }
    public string Name { get; private set; }

    public AddCityCommand(
        int provinceId,
        string name)
    {
        ProvinceId = provinceId;
        Name = name;
    }
}
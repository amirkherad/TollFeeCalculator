using TollFeeCalculator.Application.Shared.Contracts;

namespace TollFeeCalculator.Application.Handlers.TimeAmounts.Add;

public class AddTimeAmountCommand 
    : ICommand<AddTimeAmountCommandResult>
{
    public int CityId { get; set; }
    public List<AddTimeAmountDto> TimeAmounts { get; set; }
}
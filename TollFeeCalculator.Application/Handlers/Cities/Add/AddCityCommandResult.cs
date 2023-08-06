namespace TollFeeCalculator.Application.Handlers.Cities.Add;

public class AddCityCommandResult
{
    public int Id { get; set; }
    public int ProvinceId { get; set; }
    public string Name { get; set; }
}
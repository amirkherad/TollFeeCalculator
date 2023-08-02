using TollFeeCalculator.Domain.Entities.Cities;

namespace TollFeeCalculator.Domain.Entities.Provinces;

public partial class Province 
{
    public string Name { get; private set; }
    
    public ICollection<City> Cities { get; private set; }
}
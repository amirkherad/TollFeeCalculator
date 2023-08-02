using TollFeeCalculator.Domain.Contracts;
using TollFeeCalculator.Domain.Shared.BaseClasses;

namespace TollFeeCalculator.Domain.Entities.VehicleTypes;

public partial class VehicleType
    : BaseEntity, IEntity
{
    public VehicleType()
    {
    }
    
    public VehicleType(string name)
    {
        // TODO: Validation
        
        Name = name;
        
        // TODO: Raise event
    }
}
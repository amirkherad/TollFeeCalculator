using MediatR;
using Microsoft.AspNetCore.Mvc;
using TollFeeCalculator.Application.Handlers.VehicleTypes.Add;
using TollFeeCalculator.Application.Handlers.VehicleTypes.Get;

namespace TollFeeCalculator.Presentation.Controllers;

/// <summary>
/// Creates apis of vehicle type
/// </summary>
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class VehicleTypeController 
    : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// The default constructor
    /// </summary>
    /// <param name="mediator"></param>
    public VehicleTypeController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Adds new <see cref="addVehicleTypeCommand"/>
    /// </summary>
    /// <param name="addVehicleTypeCommand"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(AddVehicleTypeCommandResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> Add(
        [FromBody] AddVehicleTypeCommand addVehicleTypeCommand,
        CancellationToken cancellationToken)
    {
        var addVehicleTypeCommandResult = await _mediator.Send(
            addVehicleTypeCommand, 
            cancellationToken);

        return Ok(addVehicleTypeCommandResult);
    }
    
    /// <summary>
    /// Gets list of vehicleTypes
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<GetVehicleTypesQueryResult>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var getVehicleTypesQueryResult = await _mediator.Send(
            request: new GetVehicleTypesQuery(), 
            cancellationToken);

        return Ok(getVehicleTypesQueryResult);
    }
}
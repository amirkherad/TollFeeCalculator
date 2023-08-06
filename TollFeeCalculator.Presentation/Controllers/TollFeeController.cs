using MediatR;
using Microsoft.AspNetCore.Mvc;
using TollFeeCalculator.Application.Handlers.TollFee.Get;

namespace TollFeeCalculator.Presentation.Controllers;

/// <summary>
/// Creates apis of toll fee
/// </summary>
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class TollFeeController 
    : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// The default constructor
    /// </summary>
    /// <param name="mediator"></param>
    public TollFeeController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Gets top toll fee plans for each vehicle types of the city
    /// 0 amount means the vehicle is exempt of toll fee
    /// </summary>
    /// <param name="cityId"></param>
    /// <param name="vehicleTypeId"></param>
    /// <param name="dateTimes"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("cities/{cityId}/vehicleTypes/{vehicleTypeId}")]
    [ProducesResponseType(typeof(GetVehicleTypeTollFeeQueryResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(
        [FromRoute] int cityId,
        [FromRoute] int vehicleTypeId,
        [FromBody] DateTime[] dateTimes,
        CancellationToken cancellationToken)
    {
        var getVehicleTypeTollFeeQuery = new GetVehicleTypeTollFeeQuery(
            cityId, 
            vehicleTypeId, 
            dateTimes);
        
        var getVehicleTypeTollFeeQueryResult = await _mediator.Send(
            getVehicleTypeTollFeeQuery, 
            cancellationToken);

        return Ok(getVehicleTypeTollFeeQueryResult);
    }
}
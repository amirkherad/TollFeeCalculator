using MediatR;
using Microsoft.AspNetCore.Mvc;
using TollFeeCalculator.Application.Handlers.TimeAmounts.Add;
using TollFeeCalculator.Application.Handlers.TimeAmounts.Get;

namespace TollFeeCalculator.Presentation.Controllers;

/// <summary>
/// Creates apis of time amount
/// </summary>
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class TimeAmountController 
    : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// The default constructor
    /// </summary>
    /// <param name="mediator"></param>
    public TimeAmountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Add new toll fee plan for the city
    /// </summary>
    /// <param name="cityId"></param>
    /// <param name="addTimeAmountCommand"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("cities/{cityId}")]
    [ProducesResponseType(typeof(AddTimeAmountCommandResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> AddTimeAmount(
        [FromRoute] int cityId,
        [FromBody] AddTimeAmountCommand addTimeAmountCommand,
        CancellationToken cancellationToken)
    {
        if (cityId != addTimeAmountCommand.CityId)
            return BadRequest();
        
        var addTollFeeCommandResult = await _mediator.Send(
            addTimeAmountCommand, 
            cancellationToken);

        return Ok(addTollFeeCommandResult);
    }
    
    /// <summary>
    /// Gets top list of time amount of city
    /// </summary>
    /// <param name="cityId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("cities/{cityId}")]
    [ProducesResponseType(typeof(IReadOnlyList<GetTimeAmountQueryResult>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(
        [FromRoute] int cityId,
        CancellationToken cancellationToken)
    {
        var getTimeAmountQuery = new GetTimeAmountsQuery(cityId);
        
        var getTimeAmountQueryResults = await _mediator.Send(
            getTimeAmountQuery, 
            cancellationToken);

        return Ok(getTimeAmountQueryResults);
    }
    
    /// <summary>
    /// Gets top list of time amount of city that doesn't have vehicleType
    /// and top time amount of specific vehicleType
    /// </summary>
    /// <param name="cityId"></param>
    /// <param name="vehicleTypeId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("cities/{cityId}/vehicleTypes/{vehicleTypeId}")]
    [ProducesResponseType(typeof(GetTimeAmountQueryResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(
        [FromRoute] int cityId,
        [FromRoute] int vehicleTypeId,
        CancellationToken cancellationToken)
    {
        var getTimeAmountQuery = new GetTimeAmountQuery(
            cityId, 
            vehicleTypeId);
        
        var getTimeAmountQueryResults = await _mediator.Send(
            getTimeAmountQuery, 
            cancellationToken);

        return Ok(getTimeAmountQueryResults);
    }
}
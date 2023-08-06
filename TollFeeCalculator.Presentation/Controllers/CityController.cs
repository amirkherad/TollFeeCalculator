using MediatR;
using Microsoft.AspNetCore.Mvc;
using TollFeeCalculator.Application.Handlers.Cities.Add;
using TollFeeCalculator.Application.Handlers.Cities.Get;

namespace TollFeeCalculator.Presentation.Controllers;

/// <summary>
/// Creates apis of city
/// </summary>
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class CityController 
    : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// The default constructor
    /// </summary>
    /// <param name="mediator"></param>
    public CityController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Add new city
    /// </summary>
    /// <param name="provinceId"></param>
    /// <param name="name"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("provinces/{provinceId}")]
    [ProducesResponseType(typeof(AddCityCommandResult), StatusCodes.Status200OK)]
    public virtual async Task<IActionResult> Add(
        [FromRoute] int provinceId,
        [FromBody] string name,
        CancellationToken cancellationToken)
    {
        var addCityCommand = new AddCityCommand(
            provinceId, 
            name);
        
        var addCityCommandResult = await _mediator.Send(
            addCityCommand, 
            cancellationToken);

        return Ok(addCityCommandResult);
    }
    
    /// <summary>
    /// Gets list of cities
    /// </summary>
    /// <param name="provinceId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("provinces/{provinceId}")]
    [ProducesResponseType(typeof(IReadOnlyList<GetCitiesQueryResult>), StatusCodes.Status200OK)]
    public virtual async Task<IActionResult> Get(
        [FromRoute] int provinceId,
        CancellationToken cancellationToken)
    {
        var getCitiesQuery = new GetCitiesQuery(provinceId);
        
        var getCitiesQueryResult = await _mediator.Send(
            getCitiesQuery, 
            cancellationToken);

        return Ok(getCitiesQueryResult);
    }
}
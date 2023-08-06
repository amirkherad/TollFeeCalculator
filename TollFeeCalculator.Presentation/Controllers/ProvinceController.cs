using MediatR;
using Microsoft.AspNetCore.Mvc;
using TollFeeCalculator.Application.Handlers.Provinces.Add;
using TollFeeCalculator.Application.Handlers.Provinces.Get;

namespace TollFeeCalculator.Presentation.Controllers;

/// <summary>
/// Creates apis of province
/// </summary>
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class ProvinceController 
    : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// The default constructor
    /// </summary>
    /// <param name="mediator"></param>
    public ProvinceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Adds new province
    /// </summary>
    /// <param name="addProvinceCommand"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(AddProvinceCommandResult), StatusCodes.Status200OK)]
    public virtual async Task<IActionResult> Add(
        [FromBody] AddProvinceCommand addProvinceCommand,
        CancellationToken cancellationToken)
    {
        var addProvinceCommandResult = await _mediator.Send(
            addProvinceCommand, 
            cancellationToken);

        return Ok(addProvinceCommandResult);
    }
    
    /// <summary>
    /// Gets list of provinces
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<GetProvincesQueryResult>), StatusCodes.Status200OK)]
    public virtual async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var getProvincesQueryResults = await _mediator.Send(
            request: new GetProvincesQuery(), 
            cancellationToken);

        return Ok(getProvincesQueryResults);
    }
}
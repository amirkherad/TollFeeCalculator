using MediatR;
using Microsoft.AspNetCore.Mvc;
using TollFeeCalculator.Application.Handlers.SingleChargeRules.Add;
using TollFeeCalculator.Application.Handlers.SingleChargeRules.Get;

namespace TollFeeCalculator.Presentation.Controllers;

/// <summary>
/// Creates apis of single charge rule
/// </summary>
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class SingleChargeRuleController 
    : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// The default constructor
    /// </summary>
    /// <param name="mediator"></param>
    public SingleChargeRuleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Add new list of single charge rule for the city
    /// </summary>
    /// <param name="cityId"></param>
    /// <param name="addSingleChargeRuleCommand"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("cities/{cityId}")]
    [ProducesResponseType(typeof(AddSingleChargeRuleCommandResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> Add(
        [FromRoute] int cityId,
        [FromBody] AddSingleChargeRuleCommand addSingleChargeRuleCommand,
        CancellationToken cancellationToken)
    {
        if (cityId != addSingleChargeRuleCommand.CityId)
            return BadRequest();
        
        var addTollFeeCommandResult = await _mediator.Send(
            addSingleChargeRuleCommand, 
            cancellationToken);

        return Ok(addTollFeeCommandResult);
    }
    
    /// <summary>
    /// Gets top list of single charge rules of city
    /// </summary>
    /// <param name="cityId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("cities/{cityId}")]
    [ProducesResponseType(typeof(IReadOnlyList<GetSingleChargeRuleQueryResult>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(
        [FromRoute] int cityId,
        CancellationToken cancellationToken)
    {
        var getSingleChargeRuleQuery = new GetSingleChargeRulesQuery(cityId);
        
        var getSingleChargeRuleQueryResult = await _mediator.Send(
            getSingleChargeRuleQuery, 
            cancellationToken);

        return Ok(getSingleChargeRuleQueryResult);
    }
    
    /// <summary>
    /// Gets top list of single charge rules of city that doesn't have vehicleType
    /// and top single charge rules of specific vehicleType
    /// </summary>
    /// <param name="cityId"></param>
    /// <param name="vehicleTypeId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("cities/{cityId}/vehicleTypes/{vehicleTypeId}")]
    [ProducesResponseType(typeof(GetSingleChargeRuleQueryResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(
        [FromRoute] int cityId,
        [FromRoute] int vehicleTypeId,
        CancellationToken cancellationToken)
    {
        var getSingleChargeRuleQuery = new GetSingleChargeRuleQuery(
            cityId, 
            vehicleTypeId);
        
        var getSingleChargeRuleQueryResult = await _mediator.Send(
            getSingleChargeRuleQuery, 
            cancellationToken);

        return Ok(getSingleChargeRuleQueryResult);
    }
}
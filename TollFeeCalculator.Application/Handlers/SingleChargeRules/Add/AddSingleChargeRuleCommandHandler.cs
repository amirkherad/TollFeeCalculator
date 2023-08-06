using AutoMapper;
using TollFeeCalculator.Application.Shared.Contracts;
using TollFeeCalculator.Domain.Entities.SingleChargeRules;
using TollFeeCalculator.Infrastructure.DataAccess.Contracts;

namespace TollFeeCalculator.Application.Handlers.SingleChargeRules.Add;

public class AddSingleChargeRuleCommandHandler 
    : ICommandHandler<AddSingleChargeRuleCommand, AddSingleChargeRuleCommandResult?>
{
    private readonly IAsyncUnitOfWork _asyncUnitOfWork;
    private readonly ISingleChargeRuleRepository _singleChargeRuleRepository;
    private readonly IMapper _mapper;

    public AddSingleChargeRuleCommandHandler(
        IAsyncUnitOfWork asyncUnitOfWork, 
        ISingleChargeRuleRepository singleChargeRuleRepository, 
        IMapper mapper)
    {
        _asyncUnitOfWork = asyncUnitOfWork;
        _singleChargeRuleRepository = singleChargeRuleRepository;
        _mapper = mapper;
    }

    public async Task<AddSingleChargeRuleCommandResult?> Handle(
        AddSingleChargeRuleCommand request, 
        CancellationToken cancellationToken)
    {
        foreach (var addSingleChargeRuleDto in request.SingleChargeRules)
        {
            var singleChargeRule = new SingleChargeRule(
                request.CityId, 
                addSingleChargeRuleDto.VehicleTypeId, 
                addSingleChargeRuleDto.PeriodOfTime.ToTimeSpan());

            await _singleChargeRuleRepository.Add(
                singleChargeRule, 
                cancellationToken);
        }
        
        await _asyncUnitOfWork.SaveChanges(cancellationToken);

        return _mapper.Map<AddSingleChargeRuleCommandResult>(request);
    }
}
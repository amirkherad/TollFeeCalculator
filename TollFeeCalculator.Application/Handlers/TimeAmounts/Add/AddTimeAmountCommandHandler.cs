using AutoMapper;
using TollFeeCalculator.Application.Shared.Contracts;
using TollFeeCalculator.Domain.Entities.TimeAmounts;
using TollFeeCalculator.Infrastructure.DataAccess.Contracts;

namespace TollFeeCalculator.Application.Handlers.TimeAmounts.Add;

public class AddTimeAmountCommandHandler 
    : ICommandHandler<AddTimeAmountCommand, AddTimeAmountCommandResult?>
{
    private readonly IAsyncUnitOfWork _asyncUnitOfWork;
    private readonly ITimeAmountRepository _timeAmountRepository;
    private readonly IMapper _mapper;

    public AddTimeAmountCommandHandler(
        IAsyncUnitOfWork asyncUnitOfWork, 
        ITimeAmountRepository timeAmountRepository, 
        IMapper mapper)
    {
        _asyncUnitOfWork = asyncUnitOfWork;
        _timeAmountRepository = timeAmountRepository;
        _mapper = mapper;
    }

    public async Task<AddTimeAmountCommandResult?> Handle(
        AddTimeAmountCommand request, 
        CancellationToken cancellationToken)
    {
        foreach (var addTimeAmountDto in request.TimeAmounts)
        {
            var timeAmount = new TimeAmount(
                request.CityId, 
                addTimeAmountDto.VehicleTypeId, 
                addTimeAmountDto.From.ToTimeSpan(), 
                addTimeAmountDto.To.ToTimeSpan(), 
                addTimeAmountDto.Amout);

            await _timeAmountRepository.Add(
                timeAmount, 
                cancellationToken);
        }
        
        await _asyncUnitOfWork.SaveChanges(cancellationToken);

        return _mapper.Map<AddTimeAmountCommandResult>(request);
    }
}
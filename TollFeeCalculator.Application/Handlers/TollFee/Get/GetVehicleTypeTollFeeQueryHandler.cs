using TollFeeCalculator.Application.Shared.Contracts;
using TollFeeCalculator.Domain.Entities.SingleChargeRules;
using TollFeeCalculator.Domain.Entities.TimeAmounts;

namespace TollFeeCalculator.Application.Handlers.TollFee.Get;

public class GetVehicleTypeTollFeeQueryHandler
    : IQueryHandler<GetVehicleTypeTollFeeQuery, GetVehicleTypeTollFeeQueryResult?>
{
    private readonly ISingleChargeRuleRepository _singleChargeRuleRepository;
    private readonly ITimeAmountRepository _timeAmountRepository;

    public GetVehicleTypeTollFeeQueryHandler(
        ISingleChargeRuleRepository singleChargeRuleRepository,
        ITimeAmountRepository timeAmountRepository)
    {
        _singleChargeRuleRepository = singleChargeRuleRepository;
        _timeAmountRepository = timeAmountRepository;
    }

    public async Task<GetVehicleTypeTollFeeQueryResult?> Handle(
        GetVehicleTypeTollFeeQuery request,
        CancellationToken cancellationToken)
    {
        var timeAmountDtos = await FetchTimeAmountsFromDatabase(
            request,
            cancellationToken);

        if (timeAmountDtos.Count == 0)
            return new GetVehicleTypeTollFeeQueryResult(0);

        var tollFee = ApplySingleChargeRuleToTollFees(timeAmountDtos);

        return new GetVehicleTypeTollFeeQueryResult(tollFee);
    }

    private static long ApplySingleChargeRuleToTollFees(List<TimeAmountDto> timeAmountDtos)
    {
        long tollFee = 0;

        var orderedTimeAmounts = timeAmountDtos
            .OrderBy(x => x.CreatedOn)
            .ToList();

        var firstTimeAmount = orderedTimeAmounts.First();
        var highestAmount = firstTimeAmount.Amount;
        var firstCreatedAtInCollectionOfSingleChargeRule = firstTimeAmount.CreatedOn.Ticks;
        long? lastRecordSingleChargeRule = null;

        var lastTimeAmount = orderedTimeAmounts.Last();

        foreach (var timeAmountDto in orderedTimeAmounts)
        {
            var noSingleChargeRuleWasFound = timeAmountDto.PeriodOfTime == TimeSpan.Zero;
            var noSingleChargeRuleWasFoundForLastRecord = lastRecordSingleChargeRule == 0;

            if (noSingleChargeRuleWasFound || noSingleChargeRuleWasFoundForLastRecord)
            {
                if (lastRecordSingleChargeRule is null) // For skipping first summarise
                {
                    highestAmount = timeAmountDto.Amount; // Set new highest with current record amount for use in later
                    lastRecordSingleChargeRule = 0;
                    continue;
                }

                tollFee += highestAmount; // Add previous amount to toll fee
                highestAmount = timeAmountDto.Amount; // Set new highest with current record amount for use in later
                firstCreatedAtInCollectionOfSingleChargeRule = timeAmountDto.CreatedOn.Ticks; // Set firstCreatedAt with current createdAt to new process
                lastRecordSingleChargeRule = timeAmountDto.PeriodOfTime.Ticks;
                continue;
            }

            var singleChargeRuleWasChanged = lastRecordSingleChargeRule != timeAmountDto.PeriodOfTime.Ticks;

            if (singleChargeRuleWasChanged)
                lastRecordSingleChargeRule = timeAmountDto.PeriodOfTime.Ticks; // Process current and next records with new singleChargeRule

            var maxValidTimeOfDay = firstCreatedAtInCollectionOfSingleChargeRule + lastRecordSingleChargeRule;
            var singleChargeRuleTimeWasFinished = timeAmountDto.CreatedOn.Ticks > maxValidTimeOfDay;

            if (singleChargeRuleTimeWasFinished)
            {
                tollFee += highestAmount; // Add previous amounts to toll fee
                highestAmount = timeAmountDto.Amount; // Set new highest with current record amount for use in later
                firstCreatedAtInCollectionOfSingleChargeRule = timeAmountDto.CreatedOn.Ticks; // Set firstCreatedAt with current createdAt to new process
                
                if (timeAmountDto.Id != lastTimeAmount.Id)
                    continue;
            }

            if (timeAmountDto.Amount > highestAmount)
                highestAmount = timeAmountDto.Amount;

            if (timeAmountDto.Id != lastTimeAmount.Id)
                continue;

            tollFee += highestAmount; // Add last amount to toll fee
        }

        return tollFee;
    }

    private async Task<List<TimeAmountDto>> FetchTimeAmountsFromDatabase(
        GetVehicleTypeTollFeeQuery request,
        CancellationToken cancellationToken)
    {
        var singleChargeRules = await _singleChargeRuleRepository.Get(
            request.CityId,
            request.VehicleTypeId,
            cancellationToken);

        var timeAmounts = await _timeAmountRepository.Get(
            request.CityId,
            request.VehicleTypeId,
            cancellationToken);

        var timeAmountDtos = new List<TimeAmountDto>();

        // Combine single charge rule with time amounts
        foreach (var dateTime in request.DateTimes)
        {
            // Group items in two category:
            // 1- Items with vehicleType
            // 2- Items without vehicleType
            var groupedSingleChargeRule = singleChargeRules
                .GroupBy(x => x.VehicleTypeId)
                .Select(x => new
                {
                    VehicleTypeId = x.Key,
                    Items = x.ToList()
                })
                .ToList();

            // Check category with vehicleType have any item
            var singleChargeRule = groupedSingleChargeRule
                .Where(x => x.VehicleTypeId is not null)
                .SelectMany(x => x.Items)
                .OrderByDescending(x => x.CreatedOn)
                .FirstOrDefault(x => dateTime >= x.CreatedOn); // Rule must be defined before dateTime

            // According to dateTime there wasn't any item.
            // So use newest item without vehicleType
            if (singleChargeRule is null)
            {
                singleChargeRule = groupedSingleChargeRule
                    .Where(x => x.VehicleTypeId is null)
                    .SelectMany(x => x.Items)
                    .OrderByDescending(x => x.CreatedOn)
                    .FirstOrDefault(x => dateTime >= x.CreatedOn); // Rule must be defined before dateTime
            }

            var periodOfTime = singleChargeRule?.PeriodOfTime ?? TimeSpan.Zero; // At the specific dateTime there wasn't any defined rule

            var groupedTimeAmount = timeAmounts
                .GroupBy(x => x.VehicleTypeId)
                .Select(x => new
                {
                    VehicleTypeId = x.Key,
                    Items = x.ToList()
                })
                .ToList();
            
            var timespan = dateTime.TimeOfDay;

            // Check category with vehicleType have any item
            var timeAmount = groupedTimeAmount
                .Where(x => x.VehicleTypeId is not null)
                .SelectMany(x => x.Items)
                .OrderByDescending(x => x.CreatedOn)
                .FirstOrDefault(x => timespan >= x.From && timespan <= x.To && dateTime >= x.CreatedOn); // Rule must be defined before dateTime
            
            // According to dateTime there wasn't any item.
            // So use newest item without vehicleType
            if (timeAmount is null)
            {
                timeAmount = groupedTimeAmount
                    .Where(x => x.VehicleTypeId is null)
                    .SelectMany(x => x.Items)
                    .OrderByDescending(x => x.CreatedOn)
                    .FirstOrDefault(x => timespan >= x.From && timespan <= x.To && dateTime >= x.CreatedOn); // Rule must be defined before dateTime
            }

            if (timeAmount is null) // At the specific dateTime there wasn't any defined amount. It means no toll fee needed
                continue;

            var timeAmountDto = new TimeAmountDto(
                timeAmount.Id,
                timeAmount.Amount,
                periodOfTime,
                dateTime);

            timeAmountDtos.Add(timeAmountDto);
        }

        return timeAmountDtos;
    }

    private class TimeAmountDto
    {
        public int Id { get; }
        public long Amount { get; }
        public TimeSpan PeriodOfTime { get; }
        public DateTime CreatedOn { get; }

        public TimeAmountDto(
            int id,
            long amount,
            TimeSpan periodOfTime,
            DateTime createdOn)
        {
            Id = id;
            Amount = amount;
            PeriodOfTime = periodOfTime;
            CreatedOn = createdOn;
        }
    }
}
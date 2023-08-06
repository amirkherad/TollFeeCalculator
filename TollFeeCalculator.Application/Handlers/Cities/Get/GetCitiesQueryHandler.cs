using AutoMapper;
using TollFeeCalculator.Application.Shared.Contracts;
using TollFeeCalculator.Domain.Entities.Cities;

namespace TollFeeCalculator.Application.Handlers.Cities.Get;

public class GetCitiesQueryHandler 
    : IQueryHandler<GetCitiesQuery, IReadOnlyList<GetCitiesQueryResult>>
{
    private readonly ICityRepository _cityRepository;
    private readonly IMapper _mapper;

    public GetCitiesQueryHandler(
        ICityRepository cityRepository, 
        IMapper mapper)
    {
        _cityRepository = cityRepository;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<GetCitiesQueryResult>?> Handle(
        GetCitiesQuery request, 
        CancellationToken cancellationToken)
    {
        var cities = await _cityRepository.Get(
            request.ProvinceId, 
            cancellationToken);

        return _mapper.Map<IReadOnlyList<GetCitiesQueryResult>>(cities);
    }
}
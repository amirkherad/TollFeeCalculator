using AutoMapper;
using TollFeeCalculator.Application.Shared.Contracts;
using TollFeeCalculator.Domain.Entities.Provinces;

namespace TollFeeCalculator.Application.Handlers.Provinces.Get;

public class GetProvincesQueryHandler 
    : IQueryHandler<GetProvincesQuery, IReadOnlyList<GetProvincesQueryResult>>
{
    private readonly IProvinceRepository _provinceRepository;
    private readonly IMapper _mapper;

    public GetProvincesQueryHandler(
        IProvinceRepository provinceRepository, 
        IMapper mapper)
    {
        _provinceRepository = provinceRepository;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<GetProvincesQueryResult>?> Handle(
        GetProvincesQuery request, 
        CancellationToken cancellationToken)
    {
        var provinces = await _provinceRepository.Get(cancellationToken);

        return _mapper.Map<IReadOnlyList<GetProvincesQueryResult>>(provinces);
    }
}
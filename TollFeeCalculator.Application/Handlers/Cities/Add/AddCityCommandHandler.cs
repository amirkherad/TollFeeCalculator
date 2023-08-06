using AutoMapper;
using TollFeeCalculator.Application.Shared.Contracts;
using TollFeeCalculator.Domain.Entities.Cities;
using TollFeeCalculator.Domain.Entities.Cities.Rules.Contracts;
using TollFeeCalculator.Infrastructure.DataAccess.Contracts;

namespace TollFeeCalculator.Application.Handlers.Cities.Add;

public class AddCityCommandHandler 
    : ICommandHandler<AddCityCommand, AddCityCommandResult?>
{
    private readonly IProvinceExistingChecker _provinceExistingChecker;
    private readonly ICityNameUniquenessChecker _cityNameUniquenessChecker;
    private readonly ICityRepository _cityRepository;
    private readonly IAsyncUnitOfWork _asyncUnitOfWork;
    private readonly IMapper _mapper;

    public AddCityCommandHandler(
        ICityRepository cityRepository, 
        IAsyncUnitOfWork asyncUnitOfWork, 
        IMapper mapper, 
        IProvinceExistingChecker provinceExistingChecker, 
        ICityNameUniquenessChecker cityNameUniquenessChecker)
    {
        _cityRepository = cityRepository;
        _asyncUnitOfWork = asyncUnitOfWork;
        _mapper = mapper;
        _provinceExistingChecker = provinceExistingChecker;
        _cityNameUniquenessChecker = cityNameUniquenessChecker;
    }

    public async Task<AddCityCommandResult?> Handle(
        AddCityCommand addCityCommand, 
        CancellationToken cancellationToken)
    {
        var city = new City(
            _provinceExistingChecker,
            _cityNameUniquenessChecker,
            addCityCommand.ProvinceId, 
            addCityCommand.Name);

        await _cityRepository.Add(
            city, 
            cancellationToken);

        await _asyncUnitOfWork.SaveChanges(cancellationToken);
        
        return _mapper.Map<AddCityCommandResult>(city);
    }
}
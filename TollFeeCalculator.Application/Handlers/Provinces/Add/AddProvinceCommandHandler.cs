using AutoMapper;
using TollFeeCalculator.Application.Shared.Contracts;
using TollFeeCalculator.Domain.Entities.Provinces;
using TollFeeCalculator.Domain.Entities.Provinces.Rules.Contracts;
using TollFeeCalculator.Infrastructure.DataAccess.Contracts;

namespace TollFeeCalculator.Application.Handlers.Provinces.Add;

public class AddProvinceCommandHandler 
    : ICommandHandler<AddProvinceCommand, AddProvinceCommandResult?>
{
    private readonly IProvinceNameUniquenessChecker _provinceNameUniquenessChecker;
    private readonly IProvinceRepository _provinceRepository;
    private readonly IAsyncUnitOfWork _asyncUnitOfWork;
    private readonly IMapper _mapper;

    public AddProvinceCommandHandler(
        IProvinceRepository provinceRepository, 
        IAsyncUnitOfWork asyncUnitOfWork, 
        IMapper mapper, 
        IProvinceNameUniquenessChecker provinceNameUniquenessChecker)
    {
        _provinceRepository = provinceRepository;
        _asyncUnitOfWork = asyncUnitOfWork;
        _mapper = mapper;
        _provinceNameUniquenessChecker = provinceNameUniquenessChecker;
    }

    public async Task<AddProvinceCommandResult?> Handle(
        AddProvinceCommand addProvinceCommand, 
        CancellationToken cancellationToken)
    {
        var province = new Province(
            _provinceNameUniquenessChecker,
            addProvinceCommand.Name);

        await _provinceRepository.Add(
            province, 
            cancellationToken);

        await _asyncUnitOfWork.SaveChanges(cancellationToken);
        
        return _mapper.Map<AddProvinceCommandResult>(province);
    }
}
namespace Application.Features.Examples.ExampleEntities.Queries;

public class GetCountExampleEntity : IRequestHandler<GetCountExampleEntityQuery, IDataResult<GetCountExampleEntityResponse>>
{
    private readonly IUnitOfWorkAsync _unitOfWorkAsync;
    private readonly ILocalizationService _localizationService;
    public GetCountExampleEntity(IUnitOfWorkAsync unitOfWorkAsync, ILocalizationService localizationService)
    {
        _unitOfWorkAsync = unitOfWorkAsync;
        _localizationService = localizationService;
    }
    public async Task<IDataResult<GetCountExampleEntityResponse>> Handle(GetCountExampleEntityQuery request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWorkAsync.ExampleEntityRepository.CountAsync(x=>x.IsActive ==true);
        string message = await _localizationService.GetLocalizedAsync(ExampleEntiesMessages.RecordExist, ExampleEntiesMessages.SectionName);
        return new SuccessDataResult<GetCountExampleEntityResponse>(new GetCountExampleEntityResponse(result), message);
    }
}
public class GetCountExampleEntityQuery : IRequest<IDataResult<GetCountExampleEntityResponse>> { }
public record GetCountExampleEntityResponse(int Count);


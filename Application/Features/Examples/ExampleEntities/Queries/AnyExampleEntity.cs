namespace Application.Features.Examples.ExampleEntities.Queries;

public class AnyExampleEntity : IRequestHandler<AnyExampleEntityQuery, IDataResult<AnyExampleEntityResponse>>
{
    private readonly IUnitOfWorkAsync _unitOfWorkAsync;
    private readonly ILocalizationService _localizationService;

    public AnyExampleEntity(IUnitOfWorkAsync unitOfWorkAsync, ILocalizationService localizationService)
    {
        _unitOfWorkAsync = unitOfWorkAsync;
        _localizationService = localizationService;
    }

    public async Task<IDataResult<AnyExampleEntityResponse>> Handle(AnyExampleEntityQuery request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWorkAsync.ExampleEntityRepository.AnyAsync(x => x.Id == request.Id);
        string message = await _localizationService.GetLocalizedAsync(ExampleEntiesMessages.RecordExist, ExampleEntiesMessages.SectionName);
        return new SuccessDataResult<AnyExampleEntityResponse>(new AnyExampleEntityResponse(result), message);
    }
}

public record AnyExampleEntityQuery(Guid Id) :
    IRequest<IDataResult<AnyExampleEntityResponse>>,
    ISecuredRequest
{
    public string[] Roles => [ExampleEntiesOperationClaims.Admin,ExampleEntiesOperationClaims.Read];
}

public record AnyExampleEntityResponse(bool IsExist);



namespace Application.Features.Examples.ExampleEntities.Commands;

public class CreateExampleEntity : IRequestHandler<CreateExampleEntityCommand, IDataResult<CreatedExampleEntityResponse>>
{
    private readonly IUnitOfWorkAsync _unitOfWorkAsync;
    private readonly ExampleEntityRules _exampleEntityRules;
    private readonly ILocalizationService _localizationService;

    public CreateExampleEntity(IUnitOfWorkAsync unitOfWorkAsync, ExampleEntityRules exampleEntityRules, ILocalizationService localizationService)
    {
        _unitOfWorkAsync = unitOfWorkAsync;
        _exampleEntityRules = exampleEntityRules;
        _localizationService = localizationService;
    }

    public async Task<IDataResult<CreatedExampleEntityResponse>> Handle(CreateExampleEntityCommand request, CancellationToken cancellationToken)
    {
        await _exampleEntityRules.ExampleEntityNameCanNotBeDuplicatedWhenInserted(request.Name);
        var exampleEntity = request.Adapt<ExampleEntity>();
        //exampleEntity.Id =NewId.NextSequentialGuid();
        ExampleEntity createdExampleEntity = await _unitOfWorkAsync.ExampleEntityRepository.AddAsync(exampleEntity);
        await _unitOfWorkAsync.SaveAsync();
        CreatedExampleEntityResponse createdBrandResponse = createdExampleEntity.Adapt<CreatedExampleEntityResponse>();
        string message = await _localizationService.GetLocalizedAsync(ExampleEntiesMessages.SuccessInserted, ExampleEntiesMessages.SectionName);

        return new SuccessDataResult<CreatedExampleEntityResponse>(createdBrandResponse,message);
    }
}

public record CreateExampleEntityCommand(string Name) : 
    IRequest<IDataResult<CreatedExampleEntityResponse>>, 
    ICacheRemoverRequest, ILoggableRequest,ISecuredRequest
{
    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetAllExampleEntities";

    public string[] Roles => [ExampleEntiesOperationClaims.Admin, ExampleEntiesOperationClaims.Add];
}
public record CreatedExampleEntityResponse(Guid Id, string Name) : IResponse { }

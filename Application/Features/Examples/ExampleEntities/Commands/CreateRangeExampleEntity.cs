namespace Application.Features.Examples.ExampleEntities.Commands;

public class CreateRangeExampleEntity : IRequestHandler<CreateRangeExampleEntityCommand, IDataResult<bool>>
{
    private readonly IUnitOfWorkAsync _unitOfWorkAsync;
    private readonly ExampleEntityRules _exampleEntityRules;
    private readonly ILocalizationService _localizationService;

    public CreateRangeExampleEntity(IUnitOfWorkAsync unitOfWorkAsync, ExampleEntityRules exampleEntityRules, ILocalizationService localizationService)
    {
        _unitOfWorkAsync = unitOfWorkAsync;
        _exampleEntityRules = exampleEntityRules;
        _localizationService = localizationService;
    }

    public async Task<IDataResult<bool>> Handle(CreateRangeExampleEntityCommand request, CancellationToken cancellationToken)
    {
        var examples = request.Names.Select(name => new ExampleEntity { Name = name }).ToList();
        var exampleEntity = examples.Adapt<ICollection<ExampleEntity>>();
        var createdExampleEntity = _unitOfWorkAsync.ExampleEntityRepository.AddRangeAsync(exampleEntity);
        await _unitOfWorkAsync.SaveAsync();
        string message = await _localizationService.GetLocalizedAsync(ExampleEntiesMessages.SuccessInserted, ExampleEntiesMessages.SectionName);

        return new SuccessDataResult<bool>(message);
    }
}

public class CreateRangeExampleEntityCommand : 
    IRequest<IDataResult<bool>>,
    ICacheRemoverRequest, ILoggableRequest, ISecuredRequest
{
    public List<string> Names { get; set; }
    public CreateRangeExampleEntityCommand(List<string> names)
    {
        Names = names;
    }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetAllExampleEntities";

    public string[] Roles => [ExampleEntiesOperationClaims.Admin, ExampleEntiesOperationClaims.Add];
}
public record CreatedRangeExampleEntityResponse(Guid Id, string Name) : IResponse { }

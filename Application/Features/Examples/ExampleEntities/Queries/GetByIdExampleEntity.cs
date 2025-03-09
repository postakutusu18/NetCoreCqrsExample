namespace Application.Features.Examples.ExampleEntities.Queries;

public class GetByIdExampleEntity : IRequestHandler<GetByIdExampleEntityQuery, IDataResult<GetByIdExampleEntityResponse>>
{
    private readonly IUnitOfWorkAsync _unitOfWorkAsync;
    private readonly ExampleEntityRules _exampleEntityRules;
    private readonly ILocalizationService _localizationService;
    public GetByIdExampleEntity(IUnitOfWorkAsync unitOfWorkAsync, ExampleEntityRules exampleEntityRules, ILocalizationService localizationService)
    {
        _unitOfWorkAsync = unitOfWorkAsync;
        _exampleEntityRules = exampleEntityRules;
        _localizationService = localizationService;
    }

    public async Task<IDataResult<GetByIdExampleEntityResponse>> Handle(GetByIdExampleEntityQuery request, CancellationToken cancellationToken)
    {
        ExampleEntity? exampleEntity = await _unitOfWorkAsync.ExampleEntityRepository.GetAsync(x => x.Id == request.Id);
        await _exampleEntityRules.ExampleEntityShouldExistWhenSelected(exampleEntity);

        var response = exampleEntity.Adapt<GetByIdExampleEntityResponse>();
        string message = await _localizationService.GetLocalizedAsync(ExampleEntiesMessages.SuccessRecord, ExampleEntiesMessages.SectionName);
        return new SuccessDataResult<GetByIdExampleEntityResponse>(response,message);
    }
}
public class GetByIdExampleEntityQuery : IRequest<IDataResult<GetByIdExampleEntityResponse>>
{
    public Guid Id { get; set; }
}
public record GetByIdExampleEntityResponse(Guid Id, string Name) { }

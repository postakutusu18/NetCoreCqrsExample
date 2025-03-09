namespace Application.Features.Examples.ExampleEntities.Commands;

public class UpdateExampleEntity : IRequestHandler<UpdateExampleEntityCommand, IDataResult<UpdatedExampleEntityResponse>>
{
    private readonly IUnitOfWorkAsync _unitOfWorkAsync;
    private readonly ExampleEntityRules _exampleEntityRules;
    private readonly ILocalizationService _localizationService;

    public UpdateExampleEntity(IUnitOfWorkAsync unitOfWorkAsync, ExampleEntityRules exampleEntityRules, ILocalizationService localizationService)
    {
        _unitOfWorkAsync = unitOfWorkAsync;
        _exampleEntityRules = exampleEntityRules;
        _localizationService = localizationService;
    }

    public async Task<IDataResult<UpdatedExampleEntityResponse>> Handle(UpdateExampleEntityCommand request, CancellationToken cancellationToken)
    {
        ExampleEntity? exampleEntity = await _unitOfWorkAsync.ExampleEntityRepository.GetAsync(predicate: p => p.Id == request.Id, cancellationToken: cancellationToken, enableTracking: false);
        await _exampleEntityRules.ExampleEntityShouldExistWhenSelected(exampleEntity);
        await _exampleEntityRules.ExampleEntityNameCanNotBeDuplicatedWhenUpdated(exampleEntity);

        var mappedExampleEntity = request.Adapt(exampleEntity);
        await _unitOfWorkAsync.ExampleEntityRepository.UpdateAsync(entity: mappedExampleEntity!);
        await _unitOfWorkAsync.SaveAsync();
        var result = mappedExampleEntity.Adapt<UpdatedExampleEntityResponse>();
        string message = await _localizationService.GetLocalizedAsync(ExampleEntiesMessages.SuccessUpdated, ExampleEntiesMessages.SectionName);
        return new SuccessDataResult<UpdatedExampleEntityResponse>(result,message);
    }
}

public record UpdateExampleEntityCommand(Guid Id, string Name) : IRequest<IDataResult<UpdatedExampleEntityResponse>>;
public record UpdatedExampleEntityResponse(Guid Id, string name) : IResponse { }

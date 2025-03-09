namespace Application.Features.Examples.ExampleEntities.Commands;

public class DeleteExampleEntity : IRequestHandler<DeleteExampleEntityCommand, IDataResult<DeletedExampleEntityResponse>>
{
    private readonly IUnitOfWorkAsync _unitOfWorkAsync;
    private readonly ExampleEntityRules _exampleEntityRules;
    private readonly ILocalizationService _localizationService;

    public DeleteExampleEntity(IUnitOfWorkAsync unitOfWorkAsync, ExampleEntityRules exampleEntityRules, ILocalizationService localizationService)
    {
        _unitOfWorkAsync = unitOfWorkAsync;
        _exampleEntityRules = exampleEntityRules;
        _localizationService = localizationService;
    }

    public async Task<IDataResult<DeletedExampleEntityResponse>> Handle(DeleteExampleEntityCommand request, CancellationToken cancellationToken)
    {
        ExampleEntity? exampleEntity = await _unitOfWorkAsync.ExampleEntityRepository.GetAsync(predicate: p => p.Id == request.Id, cancellationToken: cancellationToken);
        await _exampleEntityRules.ExampleEntityShouldExistWhenSelected(exampleEntity);
        await _unitOfWorkAsync.ExampleEntityRepository.DeleteAsync(entity: exampleEntity!);
        await _unitOfWorkAsync.SaveAsync();
        var result = exampleEntity.Adapt<DeletedExampleEntityResponse>();
        string message = await _localizationService.GetLocalizedAsync(ExampleEntiesMessages.SuccessDeleted, ExampleEntiesMessages.SectionName);
        return new SuccessDataResult<DeletedExampleEntityResponse>(result,message);

    }
}

public record DeleteExampleEntityCommand(Guid Id) : IRequest<IDataResult<DeletedExampleEntityResponse>>;
public record DeletedExampleEntityResponse(Guid Id) : IResponse { }

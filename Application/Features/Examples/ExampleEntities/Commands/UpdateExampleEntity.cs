namespace Application.Features.Examples.ExampleEntities.Commands;

public class UpdateExampleEntity : IRequestHandler<UpdateExampleEntityCommand, IDataResult<UpdatedExampleEntityResponse>>
{
    private readonly IUnitOfWorkAsync _unitOfWorkAsync;
    private readonly ExampleEntityRules _exampleEntityRules;

    public UpdateExampleEntity(IUnitOfWorkAsync unitOfWorkAsync, ExampleEntityRules exampleEntityRules)
    {
        _unitOfWorkAsync = unitOfWorkAsync;
        _exampleEntityRules = exampleEntityRules;
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
        return new SuccessDataResult<UpdatedExampleEntityResponse>(result);
    }
}

public record UpdateExampleEntityCommand(Guid Id, string Name) : IRequest<IDataResult<UpdatedExampleEntityResponse>>;
public record UpdatedExampleEntityResponse(Guid Id, string name) : IResponse { }

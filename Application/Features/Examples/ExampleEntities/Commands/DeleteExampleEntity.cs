namespace Application.Features.Examples.ExampleEntities.Commands;

public class DeleteExampleEntity : IRequestHandler<DeleteExampleEntityCommand, IDataResult<DeletedExampleEntityResponse>>
{
    private readonly IUnitOfWorkAsync _unitOfWorkAsync;
    private readonly ExampleEntityRules _exampleEntityRules;

    public DeleteExampleEntity(IUnitOfWorkAsync unitOfWorkAsync, ExampleEntityRules exampleEntityRules)
    {
        _unitOfWorkAsync = unitOfWorkAsync;
        _exampleEntityRules = exampleEntityRules;
    }

    public async Task<IDataResult<DeletedExampleEntityResponse>> Handle(DeleteExampleEntityCommand request, CancellationToken cancellationToken)
    {
        ExampleEntity? exampleEntity = await _unitOfWorkAsync.ExampleEntityRepository.GetAsync(predicate: p => p.Id == request.Id, cancellationToken: cancellationToken);
        await _exampleEntityRules.ExampleEntityShouldExistWhenSelected(exampleEntity);
        await _unitOfWorkAsync.ExampleEntityRepository.DeleteAsync(entity: exampleEntity!);
        await _unitOfWorkAsync.SaveAsync();
        var result = exampleEntity.Adapt<DeletedExampleEntityResponse>();
        return new SuccessDataResult<DeletedExampleEntityResponse>(result);

    }
}

public record DeleteExampleEntityCommand(Guid Id) : IRequest<IDataResult<DeletedExampleEntityResponse>>;
public record DeletedExampleEntityResponse(Guid Id) : IResponse { }

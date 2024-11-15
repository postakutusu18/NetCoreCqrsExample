namespace Application.Features.Examples.ExampleEntities.Commands.Delete;

public class DeleteExampleEntityHandler : IRequestHandler<DeleteExampleEntityCommand, IDataResult<DeletedExampleEntityResponse>>
{
    private readonly IUnitOfWorkAsync _unitOfWorkAsync;
    private readonly ExampleEntityRules _exampleEntityRules;

    public DeleteExampleEntityHandler(IUnitOfWorkAsync unitOfWorkAsync, ExampleEntityRules exampleEntityRules)
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
namespace Application.Features.Examples.ExampleEntities.Commands.Update;

public class UpdateExampleEntityHandler : IRequestHandler<UpdateExampleEntityCommand, IDataResult<UpdatedExampleEntityResponse>>
{
    private readonly IUnitOfWorkAsync _unitOfWorkAsync;
    private readonly ExampleEntityRules _exampleEntityRules;

    public UpdateExampleEntityHandler(IUnitOfWorkAsync unitOfWorkAsync, ExampleEntityRules exampleEntityRules)
    {
        _unitOfWorkAsync = unitOfWorkAsync;
        _exampleEntityRules = exampleEntityRules;
    }

    public async Task<IDataResult<UpdatedExampleEntityResponse>> Handle(UpdateExampleEntityCommand request, CancellationToken cancellationToken)
    {
        ExampleEntity? exampleEntity = await _unitOfWorkAsync.ExampleEntityRepository.GetAsync(predicate: p => p.Id == request.Id, cancellationToken: cancellationToken, enableTracking: false);
        await _exampleEntityRules.ExampleEntityShouldExistWhenSelected(exampleEntity);
        await _exampleEntityRules.ExampleEntityNameCanNotBeDuplicatedWhenUpdated(exampleEntity);

        ExampleEntity mappedExampleEntity = request.Adapt<ExampleEntity>();
        await _unitOfWorkAsync.ExampleEntityRepository.UpdateAsync(entity: mappedExampleEntity!);
        await _unitOfWorkAsync.SaveAsync();
        var result = mappedExampleEntity.Adapt<UpdatedExampleEntityResponse>();
        return new SuccessDataResult<UpdatedExampleEntityResponse>(result);
    }
}
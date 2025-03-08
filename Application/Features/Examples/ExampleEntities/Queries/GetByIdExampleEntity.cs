namespace Application.Features.Examples.ExampleEntities.Queries;

public class GetByIdExampleEntity : IRequestHandler<GetByIdExampleEntityQuery, IDataResult<GetByIdExampleEntityResponse>>
{
    private readonly IUnitOfWorkAsync _unitOfWorkAsync;
    private readonly ExampleEntityRules _exampleEntityRules;
    public GetByIdExampleEntity(IUnitOfWorkAsync unitOfWorkAsync, ExampleEntityRules exampleEntityRules)
    {
        _unitOfWorkAsync = unitOfWorkAsync;
        _exampleEntityRules = exampleEntityRules;
    }

    public async Task<IDataResult<GetByIdExampleEntityResponse>> Handle(GetByIdExampleEntityQuery request, CancellationToken cancellationToken)
    {
        ExampleEntity? exampleEntity = await _unitOfWorkAsync.ExampleEntityRepository.GetAsync(x => x.Id == request.Id);
        await _exampleEntityRules.ExampleEntityShouldExistWhenSelected(exampleEntity);

        var response = exampleEntity.Adapt<GetByIdExampleEntityResponse>();
        return new SuccessDataResult<GetByIdExampleEntityResponse>(response);
    }
}
public class GetByIdExampleEntityQuery : IRequest<IDataResult<GetByIdExampleEntityResponse>>
{
    public Guid Id { get; set; }
}
public record GetByIdExampleEntityResponse(Guid Id, string Name) { }

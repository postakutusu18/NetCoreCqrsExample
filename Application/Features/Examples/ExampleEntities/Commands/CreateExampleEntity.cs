namespace Application.Features.Examples.ExampleEntities.Commands;

public class CreateExampleEntity : IRequestHandler<CreateExampleEntityCommand, IDataResult<CreatedExampleEntityResponse>>
{
    private readonly IUnitOfWorkAsync _unitOfWorkAsync;
    private readonly ExampleEntityRules _exampleEntityRules;

    public CreateExampleEntity(IUnitOfWorkAsync unitOfWorkAsync, ExampleEntityRules exampleEntityRules)
    {
        _unitOfWorkAsync = unitOfWorkAsync;
        _exampleEntityRules = exampleEntityRules;
    }

    public async Task<IDataResult<CreatedExampleEntityResponse>> Handle(CreateExampleEntityCommand request, CancellationToken cancellationToken)
    {
        await _exampleEntityRules.ExampleEntityNameCanNotBeDuplicatedWhenInserted(request.Name);
        var exampleEntity = request.Adapt<ExampleEntity>();
        ExampleEntity createdExampleEntity = await _unitOfWorkAsync.ExampleEntityRepository.AddAsync(exampleEntity);
        await _unitOfWorkAsync.SaveAsync();
        CreatedExampleEntityResponse createdBrandResponse = createdExampleEntity.Adapt<CreatedExampleEntityResponse>();

        return new SuccessDataResult<CreatedExampleEntityResponse>(createdBrandResponse);
    }
}

public record CreateExampleEntityCommand(string Name) : IRequest<IDataResult<CreatedExampleEntityResponse>>, ICacheRemoverRequest, ILoggableRequest
{
    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetAllExampleEntities";
}
public record CreatedExampleEntityResponse(Guid Id, string Name) : IResponse { }

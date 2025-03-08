namespace Application.Features.Examples.ExampleEntities.Commands;

public class CreateRangeExampleEntity : IRequestHandler<CreateRangeExampleEntityCommand, IDataResult<bool>>
{
    private readonly IUnitOfWorkAsync _unitOfWorkAsync;
    private readonly ExampleEntityRules _exampleEntityRules;

    public CreateRangeExampleEntity(IUnitOfWorkAsync unitOfWorkAsync, ExampleEntityRules exampleEntityRules)
    {
        _unitOfWorkAsync = unitOfWorkAsync;
        _exampleEntityRules = exampleEntityRules;
    }

    public async Task<IDataResult<bool>> Handle(CreateRangeExampleEntityCommand request, CancellationToken cancellationToken)
    {
        var examples = request.Names.Select(name => new ExampleEntity { Name = name }).ToList();
        var exampleEntity = request.Names.Adapt<ICollection<ExampleEntity>>();
        var createdExampleEntity = _unitOfWorkAsync.ExampleEntityRepository.AddRangeAsync(exampleEntity);
        await _unitOfWorkAsync.SaveAsync();
        CreatedExampleEntityResponse createdBrandResponse = createdExampleEntity.Adapt<CreatedExampleEntityResponse>();

        return new SuccessDataResult<bool>(true);
    }
}

public class CreateRangeExampleEntityCommand : IRequest<IDataResult<bool>>, ICacheRemoverRequest, ILoggableRequest
{
    public List<string> Names { get; set; }
    public CreateRangeExampleEntityCommand(List<string> names)
    {
        Names = names;
    }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetAllExampleEntities";
}
public record CreatedRangeExampleEntityResponse(Guid Id, string Name) : IResponse { }

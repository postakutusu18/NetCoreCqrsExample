namespace Application.Features.Examples.ExampleEntities.Commands.Create;

public record CreateExampleEntityCommand(string Name) : IRequest<IDataResult<CreatedExampleEntityResponse>>, ICacheRemoverRequest, ILoggableRequest
{
    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetAllExampleEntities";
}

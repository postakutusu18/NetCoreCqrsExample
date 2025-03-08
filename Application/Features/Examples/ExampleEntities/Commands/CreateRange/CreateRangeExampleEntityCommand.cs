namespace Application.Features.Examples.ExampleEntities.Commands.CreateRange;

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

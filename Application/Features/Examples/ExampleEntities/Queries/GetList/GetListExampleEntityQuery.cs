
namespace Application.Features.Examples.ExampleEntities.Queries.GetList;

public class GetListExampleEntityQuery : IRequest<IDataResult<GetListResponse<GetListExampleEntityResponse>>>//, ICachableRequest,ISecuredRequest
{
    public PageRequest PageRequest { get; set; }
    public string[] Roles => new[] { "Write", "Add" };
    public bool BypassCache => false;
    public string CacheKey => $"GetListExampleEntitys({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetAllExampleEntitys";
    public TimeSpan? SlidingExpiration { get; }
}

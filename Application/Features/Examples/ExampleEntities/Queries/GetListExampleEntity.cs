namespace Application.Features.Examples.ExampleEntities.Queries;

public class GetListExampleEntity : IRequestHandler<GetListExampleEntityQuery, IDataResult<GetListResponse<GetListExampleEntityResponse>>>
{
    private readonly IUnitOfWorkAsync _unitOfWorkAsync;
    private readonly ExampleEntityRules _exampleEntityRules;
    public GetListExampleEntity(IUnitOfWorkAsync unitOfWorkAsync, ExampleEntityRules exampleEntityRules)
    {
        _unitOfWorkAsync = unitOfWorkAsync;
        _exampleEntityRules = exampleEntityRules;
    }

    public async Task<IDataResult<GetListResponse<GetListExampleEntityResponse>>> Handle(GetListExampleEntityQuery request, CancellationToken cancellationToken)
    {
        IPaginate<ExampleEntity> products = await _unitOfWorkAsync.ExampleEntityRepository.GetListAsync(
               index: request.PageRequest.PageIndex,
               size: request.PageRequest.PageSize
           );
        var mappedProductListModel = products.Adapt<GetListResponse<GetListExampleEntityResponse>>();
        var resultData = new SuccessDataResult<GetListResponse<GetListExampleEntityResponse>>(mappedProductListModel);
        return resultData;
    }


}

public class GetListExampleEntityQuery : IRequest<IDataResult<GetListResponse<GetListExampleEntityResponse>>>//, ICachableRequest,ISecuredRequest
{
    public PageRequest PageRequest { get; set; }
    public string[] Roles => new[] { "Write", "Add" };
    public bool BypassCache => false;
    public string CacheKey => $"GetListExampleEntitys({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetAllExampleEntitys";
    public TimeSpan? SlidingExpiration { get; }
}
public record GetListExampleEntityResponse(Guid Id, string Name) { }

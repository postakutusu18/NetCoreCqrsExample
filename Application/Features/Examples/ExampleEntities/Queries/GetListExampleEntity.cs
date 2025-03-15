
namespace Application.Features.Examples.ExampleEntities.Queries;

public class GetListExampleEntity : IRequestHandler<GetListExampleEntityQuery, IDataResult<GetListResponse<GetListExampleEntityResponse>>>
{
    private readonly IUnitOfWorkAsync _unitOfWorkAsync;
    private readonly ExampleEntityRules _exampleEntityRules;
    private readonly ILocalizationService _localizationService;
    public GetListExampleEntity(IUnitOfWorkAsync unitOfWorkAsync, ExampleEntityRules exampleEntityRules, ILocalizationService localizationService)
    {
        _unitOfWorkAsync = unitOfWorkAsync;
        _exampleEntityRules = exampleEntityRules;
        _localizationService = localizationService;
    }

    public async Task<IDataResult<GetListResponse<GetListExampleEntityResponse>>> Handle(GetListExampleEntityQuery request, CancellationToken cancellationToken)
    {
        IPaginate<ExampleEntity> products = await _unitOfWorkAsync.ExampleEntityRepository.GetListAsync(
               index: request.PageRequest.PageIndex,
               size: request.PageRequest.PageSize
           );
        var mappedProductListModel = products.Adapt<GetListResponse<GetListExampleEntityResponse>>();
        string message = await _localizationService.GetLocalizedAsync(ExampleEntiesMessages.SuccessList, ExampleEntiesMessages.SectionName);
        var resultData = new SuccessDataResult<GetListResponse<GetListExampleEntityResponse>>(mappedProductListModel,message);
        return resultData;
    }


}

public class GetListExampleEntityQuery : 
    IRequest<IDataResult<GetListResponse<GetListExampleEntityResponse>>>,
    ICachableRequest, IIntervalRequest ,ISecuredRequest
{
    public PageRequest PageRequest { get; set; }
    public string[] Roles => [ExampleEntiesOperationClaims.Admin, ExampleEntiesOperationClaims.Read];
    public bool BypassCache => false;
    public string CacheKey => $"GetAllExampleEntities({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetAllExampleEntities";
    public TimeSpan? SlidingExpiration { get; }

    public int Interval => 5;
}
public record GetListExampleEntityResponse(Guid Id, string Name) { }

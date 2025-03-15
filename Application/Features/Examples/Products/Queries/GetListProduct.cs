namespace Application.Features.Examples.Products.Queries;

public class GetListProduct : IRequestHandler<GetListProductQuery, IDataResult<GetListResponse<GetListProductResponse>>>
{
    private readonly IUnitOfWorkAsync _unitOfWorkAsync;
    private readonly ProductBusinessRules _productBusinessRules;
    private readonly ILocalizationService _localizationService;
    public GetListProduct(IUnitOfWorkAsync unitOfWorkAsync, ProductBusinessRules productBusinessRules, ILocalizationService localizationService)
    {
        _unitOfWorkAsync = unitOfWorkAsync;
        _productBusinessRules = productBusinessRules;
        _localizationService = localizationService;
    }

    public async Task<IDataResult<GetListResponse<GetListProductResponse>>> Handle(GetListProductQuery request, CancellationToken cancellationToken)
    {
        IPaginate<Product> products = await _unitOfWorkAsync.ProductRepository.GetListAsync(
               index: request.PageRequest.PageIndex,
               size: request.PageRequest.PageSize
           );
        var mappedProductListModel = products.Adapt<GetListResponse<GetListProductResponse>>();
        string message = await _localizationService.GetLocalizedAsync(ProductsMessages.SuccessRecord, ProductsMessages.SectionName);
        var resultData = new SuccessDataResult<GetListResponse<GetListProductResponse>>(mappedProductListModel,message);
        return resultData;
    }


}
public record GetListProductQuery : IRequest<IDataResult<GetListResponse<GetListProductResponse>>>//, ICachableRequest,ISecuredRequest
{
    public PageRequest PageRequest { get; set; }
    public string[] Roles => new[] { "Write", "Add" };
    public bool BypassCache => false;
    public string CacheKey => $"GetListProducts({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetAllProducts";
    public TimeSpan? SlidingExpiration { get; }
}
public record GetListProductResponse(Guid Id, string Name) { }

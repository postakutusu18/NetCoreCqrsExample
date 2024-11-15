namespace Application.Features.Example.Products.Queries.GetList;

public class GetListProductHandler : IRequestHandler<GetListProductQuery, IDataResult<GetListResponse<GetListProductResponse>>>
{
    private readonly IUnitOfWorkAsync _unitOfWorkAsync;
    private readonly ProductBusinessRules _productBusinessRules;
    public GetListProductHandler(IUnitOfWorkAsync unitOfWorkAsync, ProductBusinessRules productBusinessRules)
    {
        _unitOfWorkAsync = unitOfWorkAsync;
        _productBusinessRules = productBusinessRules;
    }

    public async Task<IDataResult<GetListResponse<GetListProductResponse>>> Handle(GetListProductQuery request, CancellationToken cancellationToken)
    {
        IPaginate<Product> products = await _unitOfWorkAsync.ProductRepository.GetListAsync(
               index: request.PageRequest.PageIndex,
               size: request.PageRequest.PageSize
           );
        var mappedProductListModel = products.Adapt<GetListResponse<GetListProductResponse>>();
        var resultData = new SuccessDataResult<GetListResponse<GetListProductResponse>>(mappedProductListModel);
        return resultData;
    }


}

using Application.Features.Products.Rules;
using Core.Application.Responses;
using Core.Application.Results;
using Core.Persistance.Paging;
using Domains;
using Mapster;
using MediatR;

namespace Application.Features.Products.Queries.GetList;

public class GetListProductHandler : IRequestHandler<GetListProductQuery, IDataResult<GetListResponse<GetListProductResponse>>>
{
    private readonly IProductRepository _productRepository;
    private readonly ProductBusinessRules _productBusinessRules;
    public GetListProductHandler(IProductRepository productRepository, ProductBusinessRules productBusinessRules)
    {
        _productRepository = productRepository;
        _productBusinessRules = productBusinessRules;
    }

    public async Task<IDataResult<GetListResponse<GetListProductResponse>>> Handle(GetListProductQuery request, CancellationToken cancellationToken)
    {
        IPaginate<Product> products = await _productRepository.GetListAsync(
               index: request.PageRequest.PageIndex,
               size: request.PageRequest.PageSize
           );
        var mappedProductListModel = products.Adapt<GetListResponse<GetListProductResponse>>();
        var resultData = new SuccessDataResult<GetListResponse<GetListProductResponse>>(mappedProductListModel);
        return resultData;
    }


}

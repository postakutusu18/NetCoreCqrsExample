using Application.Features.Products.Rules;
using Core.Application.Results;
using Domains;
using Mapster;
using MediatR;

namespace Application.Features.Products.Queries.GetById;

public class GetByIdProductHandler : IRequestHandler<GetByIdProductQuery,IDataResult<GetByIdProductResponse>>
{
    private readonly IProductRepository _productRepository;
    private readonly ProductBusinessRules _productBusinessRules;
    public GetByIdProductHandler(IProductRepository productRepository, ProductBusinessRules productBusinessRules)
    {
        _productRepository = productRepository;
        _productBusinessRules = productBusinessRules;
    }

    public async Task<IDataResult<GetByIdProductResponse>> Handle(GetByIdProductQuery request, CancellationToken cancellationToken)
    {
        Product? product = await _productRepository.GetAsync(x => x.Id == request.Id);
        _productBusinessRules.ProductShouldExistWhenSelected(product);

        GetByIdProductResponse? response = product.Adapt<GetByIdProductResponse>();
        return new SuccessDataResult<GetByIdProductResponse>(response);
    }
}

using Application.Features.Products.Rules;
using Core.Application.Responses;
using Core.Application.Results;
using Domains;
using Domains.Users;
using Mapster;
using MediatR;

namespace Application.Features.Products.Commands.Update;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, IDataResult<UpdatedProductResponse>>
{
    private readonly IProductRepository _productRepository;
    private readonly ProductBusinessRules _productBusinessRules;

    public UpdateProductHandler(IProductRepository productRepository, ProductBusinessRules productBusinessRules)
    {
        _productRepository = productRepository;
        _productBusinessRules = productBusinessRules;
    }

    public async Task<IDataResult<UpdatedProductResponse>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        Product? product = await _productRepository.GetAsync(predicate: p => p.Id == request.Id, cancellationToken: cancellationToken,enableTracking:false);
        _productBusinessRules.ProductShouldExistWhenSelected(product);
        await _productBusinessRules.ProductNameCanNotBeDuplicatedWhenUpdated(product);

        Product mappedProduct = request.Adapt<Product>();
        await _productRepository.UpdateAsync(entity: mappedProduct!);
        var result = mappedProduct.Adapt<UpdatedProductResponse>();
        return new SuccessDataResult<UpdatedProductResponse>(result);
    }
}
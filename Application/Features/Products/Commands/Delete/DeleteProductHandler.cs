using Application.Features.Products.Rules;
using Core.Application.Responses;
using Core.Application.Results;
using Domains;
using Mapster;
using MediatR;

namespace Application.Features.Products.Commands.Delete;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, IDataResult<DeletedProductResponse>>
{
    private readonly IProductRepository _productRepository;
    private readonly ProductBusinessRules _productRules;

    public DeleteProductHandler(IProductRepository productRepository, ProductBusinessRules productRules)
    {
        _productRepository = productRepository;
        _productRules = productRules;
    }

    public async Task<IDataResult<DeletedProductResponse>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        Product? product = await _productRepository.GetAsync(predicate: p => p.Id == request.Id, cancellationToken: cancellationToken);
        await _productRules.ProductShouldExistWhenSelected(product);
        await _productRepository.DeleteAsync(entity: product!);
        var result  = product.Adapt<DeletedProductResponse>();
        return new SuccessDataResult<DeletedProductResponse>(result);

    }
}

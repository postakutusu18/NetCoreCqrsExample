﻿namespace Application.Features.Examples.Products.Commands;

public class DeleteProduct : IRequestHandler<DeleteProductCommand, IDataResult<DeletedProductResponse>>
{
    private readonly IUnitOfWorkAsync _unitOfWorkAsync;
    private readonly ProductBusinessRules _productRules;

    public DeleteProduct(IUnitOfWorkAsync unitOfWorkAsync, ProductBusinessRules productRules)
    {
        _unitOfWorkAsync = unitOfWorkAsync;
        _productRules = productRules;
    }

    public async Task<IDataResult<DeletedProductResponse>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        Product? product = await _unitOfWorkAsync.ProductRepository.GetAsync(predicate: p => p.Id == request.Id, cancellationToken: cancellationToken);
        await _productRules.ProductShouldExistWhenSelected(product);
        await _unitOfWorkAsync.ProductRepository.DeleteAsync(entity: product!);
        await _unitOfWorkAsync.SaveAsync();
        var result = product.Adapt<DeletedProductResponse>();
        return new SuccessDataResult<DeletedProductResponse>(result);

    }
}
public record DeleteProductCommand(Guid Id) : IRequest<IDataResult<DeletedProductResponse>>;
public record DeletedProductResponse(Guid Id) : IResponse;
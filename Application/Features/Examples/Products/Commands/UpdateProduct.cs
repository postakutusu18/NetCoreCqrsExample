namespace Application.Features.Examples.Products.Commands;

public class UpdateProduct : IRequestHandler<UpdateProductCommand, IDataResult<UpdatedProductResponse>>
{
    private readonly IUnitOfWorkAsync _unitOfWorkAsync;
    private readonly ProductBusinessRules _productBusinessRules;

    public UpdateProduct(IUnitOfWorkAsync unitOfWorkAsync, ProductBusinessRules productBusinessRules)
    {
        _unitOfWorkAsync = unitOfWorkAsync;
        _productBusinessRules = productBusinessRules;
    }

    public async Task<IDataResult<UpdatedProductResponse>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        Product? product = await _unitOfWorkAsync.ProductRepository.GetAsync(predicate: p => p.Id == request.Id, cancellationToken: cancellationToken, enableTracking: false);
        _productBusinessRules.ProductShouldExistWhenSelected(product);
        await _productBusinessRules.ProductNameCanNotBeDuplicatedWhenUpdated(product);
        var mappedProduct = request.Adapt(product);
        await _unitOfWorkAsync.ProductRepository.UpdateAsync(entity: mappedProduct!);
        await _unitOfWorkAsync.SaveAsync();
        var result = mappedProduct.Adapt<UpdatedProductResponse>();
        return new SuccessDataResult<UpdatedProductResponse>(result);
    }
}

public record UpdateProductCommand(Guid Id, string Name) : IRequest<IDataResult<UpdatedProductResponse>>;
public record UpdatedProductResponse(Guid Id, string name) : IResponse;

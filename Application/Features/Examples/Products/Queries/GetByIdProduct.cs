namespace Application.Features.Examples.Products.Queries;

public class GetByIdProduct : IRequestHandler<GetByIdProductQuery, IDataResult<GetByIdProductResponse>>
{
    private readonly IUnitOfWorkAsync _unitOfWorkAsync;
    private readonly ProductBusinessRules _productBusinessRules;
    public GetByIdProduct(IUnitOfWorkAsync unitOfWorkAsync, ProductBusinessRules productBusinessRules)
    {
        _unitOfWorkAsync = unitOfWorkAsync;
        _productBusinessRules = productBusinessRules;
    }

    public async Task<IDataResult<GetByIdProductResponse>> Handle(GetByIdProductQuery request, CancellationToken cancellationToken)
    {
        Product? product = await _unitOfWorkAsync.ProductRepository.GetAsync(x => x.Id == request.Id);
        _productBusinessRules.ProductShouldExistWhenSelected(product);

        GetByIdProductResponse? response = product.Adapt<GetByIdProductResponse>();
        return new SuccessDataResult<GetByIdProductResponse>(response);
    }
}
public record GetByIdProductQuery(Guid Id) : IRequest<IDataResult<GetByIdProductResponse>>;
public record GetByIdProductResponse(Guid Id, string Name) { }

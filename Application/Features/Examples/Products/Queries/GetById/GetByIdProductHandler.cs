namespace Application.Features.Example.Products.Queries.GetById;

public class GetByIdProductHandler : IRequestHandler<GetByIdProductQuery, IDataResult<GetByIdProductResponse>>
{
    private readonly IUnitOfWorkAsync _unitOfWorkAsync;
    private readonly ProductBusinessRules _productBusinessRules;
    public GetByIdProductHandler(IUnitOfWorkAsync unitOfWorkAsync, ProductBusinessRules productBusinessRules)
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

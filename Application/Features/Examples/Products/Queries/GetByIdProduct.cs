namespace Application.Features.Examples.Products.Queries;

public class GetByIdProduct : IRequestHandler<GetByIdProductQuery, IDataResult<GetByIdProductResponse>>
{
    private readonly IUnitOfWorkAsync _unitOfWorkAsync;
    private readonly ProductBusinessRules _productBusinessRules;
    private readonly ILocalizationService _localizationService;
    public GetByIdProduct(IUnitOfWorkAsync unitOfWorkAsync, ProductBusinessRules productBusinessRules, ILocalizationService localizationService)
    {
        _unitOfWorkAsync = unitOfWorkAsync;
        _productBusinessRules = productBusinessRules;
        _localizationService = localizationService;
    }

    public async Task<IDataResult<GetByIdProductResponse>> Handle(GetByIdProductQuery request, CancellationToken cancellationToken)
    {
        Product? product = await _unitOfWorkAsync.ProductRepository.GetAsync(x => x.Id == request.Id);
        _productBusinessRules.ProductShouldExistWhenSelected(product);
        string message = await _localizationService.GetLocalizedAsync(ProductsMessages.SuccessRecord, ProductsMessages.SectionName);
        GetByIdProductResponse? response = product.Adapt<GetByIdProductResponse>();
        return new SuccessDataResult<GetByIdProductResponse>(response,message);
    }
}
public record GetByIdProductQuery(Guid Id) : IRequest<IDataResult<GetByIdProductResponse>>;
public record GetByIdProductResponse(Guid Id, string Name) { }

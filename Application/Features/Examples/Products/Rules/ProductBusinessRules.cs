namespace Application.Features.Example.Products.Rules;
public class ProductBusinessRules : BaseBusinessRules
{
    private readonly IUnitOfWorkAsync _unitOfWorkAsync;
    private readonly ILocalizationService _localizationService;

    public ProductBusinessRules(IUnitOfWorkAsync unitOfWorkAsync, ILocalizationService localizationService)
    {
        _unitOfWorkAsync = unitOfWorkAsync;
        _localizationService = localizationService;
    }

    public async Task ProductShouldExistWhenSelected(Product? product)
    {
        string message = await _localizationService.GetLocalizedAsync(ProductsMessages.ProductNotExists, ProductsMessages.SectionName);
        if (product == null)
            throw new BusinessException(message);
    }

    public async Task ProductIdShouldExistWhenSelected(Guid id)
    {
        Product? product = await _unitOfWorkAsync.ProductRepository.GetAsync(predicate: b => b.Id == id, enableTracking: false);
        ProductShouldExistWhenSelected(product);
    }

    public async Task ProductNameCanNotBeDuplicatedWhenInserted(string name)
    {
        string message = await _localizationService.GetLocalizedAsync(ProductsMessages.DublicateName, ProductsMessages.SectionName);
        Product? result = await _unitOfWorkAsync.ProductRepository.GetAsync(x => x.Name.ToLower() == name.ToLower());
        if (result != null)
            throw new BusinessException(message);
    }

    public async Task ProductNameCanNotBeDuplicatedWhenUpdated(Product product)
    {
        Product? result = await _unitOfWorkAsync.ProductRepository.GetAsync(x => x.Id != product.Id && x.Name.ToLower() == product.Name.ToLower());
        if (result != null)
            throw new BusinessException(ProductsMessages.ProductNameExists);
    }

    public async Task ProductNameListCanNotBeDuplicatedWhenInserted(List<string> nameList)
    {
        IPaginate<Product> result = await _unitOfWorkAsync.ProductRepository.GetListAsync(predicate: b => nameList.Contains(b.Name), enableTracking: false);
        if (result.Items.Any())
            throw new BusinessException(ProductsMessages.ProductNameExists);
    }
}

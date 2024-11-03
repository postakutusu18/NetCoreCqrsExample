using Application.Features.Auth.Constants;
using Application.Features.Products.Constants;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Localization;
using Core.Persistance.Paging;
using Domains;
using static MailKit.Net.Imap.ImapEvent;

namespace Application.Features.Products.Rules;
public class ProductBusinessRules : BaseBusinessRules
{
    private readonly IProductRepository _productRepository;
    private readonly ILocalizationService _localizationService;

    public ProductBusinessRules(IProductRepository productRepository, ILocalizationService localizationService)
    {
        _productRepository = productRepository;
        _localizationService = localizationService;
    }

    public async Task ProductShouldExistWhenSelected(Product? product)
    {
        string message =await _localizationService.GetLocalizedAsync(ProductsMessages.ProductNotExists, ProductsMessages.SectionName);
        if (product == null)
            throw new BusinessException(message);
    }

    public async Task ProductIdShouldExistWhenSelected(Guid id)
    {
        Product? product = await _productRepository.GetAsync(predicate: b => b.Id == id, enableTracking: false);
        ProductShouldExistWhenSelected(product);
    }

    public async Task ProductNameCanNotBeDuplicatedWhenInserted(string name)
    {
        string message = await _localizationService.GetLocalizedAsync(ProductsMessages.DublicateName, ProductsMessages.SectionName);
        Product? result = await _productRepository.GetAsync(x => x.Name.ToLower() == name.ToLower());
        if (result != null)
        throw new BusinessException(message);
    }

    public async Task ProductNameCanNotBeDuplicatedWhenUpdated(Product product)
    {
        Product? result = await _productRepository.GetAsync(x => x.Id != product.Id && x.Name.ToLower() == product.Name.ToLower());
        if (result != null)
            throw new BusinessException(ProductsMessages.ProductNameExists);
    }

    public async Task ProductNameListCanNotBeDuplicatedWhenInserted(List<string> nameList)
    {
        IPaginate<Product> result = await _productRepository.GetListAsync(predicate: b => nameList.Contains(b.Name), enableTracking: false);
        if (result.Items.Any())
            throw new BusinessException(ProductsMessages.ProductNameExists);
    }
}

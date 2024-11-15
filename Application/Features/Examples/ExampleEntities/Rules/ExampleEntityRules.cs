

namespace Application.Features.Examples.ExampleEntities.Rules;

public class ExampleEntityRules : BaseBusinessRules
{
    private readonly IUnitOfWorkAsync _unitOfWorkAsync;
    private readonly ILocalizationService _localizationService;

    public ExampleEntityRules(IUnitOfWorkAsync unitOfWorkAsync, ILocalizationService localizationService)
    {
        _unitOfWorkAsync = unitOfWorkAsync;
        _localizationService = localizationService;
    }

    public async Task ExampleEntityShouldExistWhenSelected(ExampleEntity? product)
    {
        string message = await _localizationService.GetLocalizedAsync(ExampleEntiesMessages.ExampleEntityNotExists, ExampleEntiesMessages.SectionName);
        if (product == null)
            throw new BusinessException(message);
    }

    public async Task ExampleEntityIdShouldExistWhenSelected(Guid id)
    {
        ExampleEntity? product = await _unitOfWorkAsync.ExampleEntityRepository.GetAsync(predicate: b => b.Id == id, enableTracking: false);
        ExampleEntityShouldExistWhenSelected(product);
    }

    public async Task ExampleEntityNameCanNotBeDuplicatedWhenInserted(string name)
    {
        string message = await _localizationService.GetLocalizedAsync(ExampleEntiesMessages.DublicateName, ExampleEntiesMessages.SectionName);
        ExampleEntity? result = await _unitOfWorkAsync.ExampleEntityRepository.GetAsync(x => x.Name.ToLower() == name.ToLower());
        if (result != null)
            throw new BusinessException(message);
    }

    public async Task ExampleEntityNameCanNotBeDuplicatedWhenUpdated(ExampleEntity product)
    {
        ExampleEntity? result = await _unitOfWorkAsync.ExampleEntityRepository.GetAsync(x => x.Id != product.Id && x.Name.ToLower() == product.Name.ToLower());
        if (result != null)
            throw new BusinessException(ExampleEntiesMessages.ExampleEntityNameExists);
    }

    public async Task ExampleEntityNameListCanNotBeDuplicatedWhenInserted(List<string> nameList)
    {
        IPaginate<ExampleEntity> result = await _unitOfWorkAsync.ExampleEntityRepository.GetListAsync(predicate: b => nameList.Contains(b.Name), enableTracking: false);
        if (result.Items.Any())
            throw new BusinessException(ExampleEntiesMessages.ExampleEntityNameExists);
    }
}

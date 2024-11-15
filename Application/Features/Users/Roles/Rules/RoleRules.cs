using Application.Features.UserFeatures.Roles.Constants;

namespace Application.Features.UserFeatures.Roles.Rules;

public class RoleRules : BaseBusinessRules
{
    private readonly IUnitOfWorkAsync _unitOfWorkAsync;
    private readonly ILocalizationService _localizationService;

    public RoleRules(
        IUnitOfWorkAsync unitOfWorkAsync,
        ILocalizationService localizationService
    )
    {
        _unitOfWorkAsync = unitOfWorkAsync;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, RoleMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task RoleShouldExistWhenSelected(Role? role)
    {
        if (role == null)
            await throwBusinessException(RoleMessages.NotExists);
    }

    public async Task RoleIdShouldExistWhenSelected(int id)
    {
        bool doesExist = await _unitOfWorkAsync.RoleRepository.AnyAsync(predicate: b => b.Id == id);
        if (doesExist)
            await throwBusinessException(RoleMessages.NotExists);
    }

    public async Task RoleNameShouldNotExistWhenCreating(string name)
    {
        bool doesExist = await _unitOfWorkAsync.RoleRepository.AnyAsync(predicate: b => b.Name == name);
        if (doesExist)
            await throwBusinessException(RoleMessages.AlreadyExists);
    }

    public async Task RoleNameShouldNotExistWhenUpdating(int id, string name)
    {
        bool doesExist = await _unitOfWorkAsync.RoleRepository.AnyAsync(predicate: b => b.Id != id && b.Name == name);
        if (doesExist)
            await throwBusinessException(RoleMessages.AlreadyExists);
    }
}
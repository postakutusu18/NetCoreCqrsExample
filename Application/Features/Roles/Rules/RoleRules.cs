﻿using Application.Features.Roles.Constants;
using Application.Repositories.Users;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Localization;
using Domains.Users;

namespace Application.Features.Roles.Rules;

public class RoleRules : BaseBusinessRules
{
    private readonly IRoleRepository _roleRepository;
    private readonly ILocalizationService _localizationService;

    public RoleRules(
        IRoleRepository roleRepository,
        ILocalizationService localizationService
    )
    {
        _roleRepository = roleRepository;
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
        bool doesExist = await _roleRepository.AnyAsync(predicate: b => b.Id == id);
        if (doesExist)
            await throwBusinessException(RoleMessages.NotExists);
    }

    public async Task RoleNameShouldNotExistWhenCreating(string name)
    {
        bool doesExist = await _roleRepository.AnyAsync(predicate: b => b.Name == name);
        if (doesExist)
            await throwBusinessException(RoleMessages.AlreadyExists);
    }

    public async Task RoleNameShouldNotExistWhenUpdating(int id, string name)
    {
        bool doesExist = await _roleRepository.AnyAsync(predicate: b => b.Id != id && b.Name == name);
        if (doesExist)
            await throwBusinessException(RoleMessages.AlreadyExists);
    }
}
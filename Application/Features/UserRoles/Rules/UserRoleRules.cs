﻿using Application.Features.UserRoles.Constants;
using Application.Repositories.Users;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Localization;
using Domains.Users;

namespace Application.Features.UserRoles.Rules;



public class UserRoleRules : BaseBusinessRules
{
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly ILocalizationService _localizationService;

    public UserRoleRules(
        IUserRoleRepository userRoleRepository,
        ILocalizationService localizationService
    )
    {
        _userRoleRepository = userRoleRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, UserRolesMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task UserRoleShouldExistWhenSelected(UserRole? userRole)
    {
        if (userRole == null)
            await throwBusinessException(UserRolesMessages.UserRoleNotExists);
    }

    public async Task UserRoleIdShouldExistWhenSelected(Guid id)
    {
        bool doesExist = await _userRoleRepository.AnyAsync(predicate: b => b.Id == id);
        if (!doesExist)
            await throwBusinessException(UserRolesMessages.UserRoleNotExists);
    }

    public async Task UserRoleShouldNotExistWhenSelected(UserRole? userRole)
    {
        if (userRole != null)
            await throwBusinessException(UserRolesMessages.UserRoleAlreadyExists);
    }

    public async Task UserShouldNotHasRoleAlreadyWhenInsert(Guid userId, int roleId)
    {
        bool doesExist = await _userRoleRepository.AnyAsync(u =>
            u.UserId == userId && u.RoleId == roleId
        );
        if (doesExist)
            await throwBusinessException(UserRolesMessages.UserRoleAlreadyExists);
    }

    public async Task UserShouldNotHasRoleAlreadyWhenUpdated(Guid id, Guid userId, int roleId)
    {
        bool doesExist = await _userRoleRepository.AnyAsync(predicate: uoc =>
            uoc.Id == id && uoc.UserId == userId && uoc.RoleId == roleId
        );
        if (doesExist)
            await throwBusinessException(UserRolesMessages.UserRoleAlreadyExists);
    }
}
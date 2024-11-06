﻿using Application.Features.Roles.Rules;
using Application.Repositories;
using Application.Repositories.Users;
using Core.Application.Results;
using Domains.Users;
using Mapster;
using MediatR;


namespace Application.Features.Roles.Commands.Delete;

public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, IDataResult<DeletedRoleResponse>>
{
    private readonly IUnitOfWorkAsync _unitOfWorkAsync;
    private readonly RoleRules _roleRules;

    public DeleteRoleCommandHandler(
        IUnitOfWorkAsync unitOfWorkAsync,
        RoleRules roleRules
    )
    {
        _unitOfWorkAsync = unitOfWorkAsync;
        _roleRules = roleRules;
    }

    public async Task<IDataResult<DeletedRoleResponse>> Handle(
        DeleteRoleCommand request,
        CancellationToken cancellationToken
    )
    {
        Role? role = await _unitOfWorkAsync.RoleRepository.GetAsync(
            predicate: oc => oc.Id == request.Id,
            cancellationToken: cancellationToken
        );
        await _roleRules.RoleShouldExistWhenSelected(role);

        await _unitOfWorkAsync.RoleRepository.DeleteAsync(entity: role!);
        await _unitOfWorkAsync.SaveAsync();

        DeletedRoleResponse response = role.Adapt<DeletedRoleResponse>();
        var result = new SuccessDataResult<DeletedRoleResponse>(response);
        return result;
    }
}
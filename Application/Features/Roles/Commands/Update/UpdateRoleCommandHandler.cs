using Application.Features.Roles.Rules;
using Application.Repositories.Users;
using Core.Application.Results;
using Domains.Users;
using Mapster;
using MediatR;

namespace Application.Features.Roles.Commands.Update;

public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand,IDataResult<UpdatedRoleResponse>>
{
    private readonly IRoleRepository _roleRepository;
    private readonly RoleRules _roleRules;

    public UpdateRoleCommandHandler(
        IRoleRepository roleRepository,
        RoleRules roleRules
    )
    {
        _roleRepository = roleRepository;
        _roleRules = roleRules;
    }

    public async Task<IDataResult<UpdatedRoleResponse>> Handle(
        UpdateRoleCommand request,
        CancellationToken cancellationToken
    )
    {
        Role? role = await _roleRepository.GetAsync(
            predicate: oc => oc.Id == request.Id,
            cancellationToken: cancellationToken
        );
        await _roleRules.RoleShouldExistWhenSelected(role);
        await _roleRules.RoleNameShouldNotExistWhenUpdating(request.Id, request.Name);
        Role mappedRole = request.Adapt<Role>();

        Role updatedRole = await _roleRepository.UpdateAsync(mappedRole);

        UpdatedRoleResponse response = updatedRole.Adapt<UpdatedRoleResponse>();
        var result = new SuccessDataResult<UpdatedRoleResponse>(response);
        return result;
    }
}
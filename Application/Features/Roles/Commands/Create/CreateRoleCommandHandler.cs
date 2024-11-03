using Application.Features.Roles.Rules;
using Application.Repositories.Users;
using Core.Application.Results;
using Domains.Users;
using Mapster;
using MediatR;

namespace Application.Features.Roles.Commands.Create;

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand,IDataResult<CreatedRoleResponse>>
{
    private readonly IRoleRepository _roleRepository;
    private readonly RoleRules _roleRules;

    public CreateRoleCommandHandler(
        IRoleRepository roleRepository,
        RoleRules roleRules
    )
    {
        _roleRepository = roleRepository;
        _roleRules = roleRules;
    }

    public async Task<IDataResult<CreatedRoleResponse>> Handle(
        CreateRoleCommand request,
        CancellationToken cancellationToken
    )
    {
        await _roleRules.RoleNameShouldNotExistWhenCreating(request.Name);
        Role mappedRole = request.Adapt<Role>();

        Role createdRole = await _roleRepository.AddAsync(mappedRole);

        CreatedRoleResponse response = createdRole.Adapt<CreatedRoleResponse>();
        var result = new SuccessDataResult<CreatedRoleResponse>(response);
        return result;
    }
}
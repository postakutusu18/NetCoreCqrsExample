using Application.Features.Roles.Rules;
using Application.Repositories.Users;
using Core.Application.Results;
using Domains.Users;
using Mapster;
using MediatR;


namespace Application.Features.Roles.Commands.Delete;

public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, IDataResult<DeletedRoleResponse>>
{
    private readonly IRoleRepository _roleRepository;
    private readonly RoleRules _roleRules;

    public DeleteRoleCommandHandler(
        IRoleRepository roleRepository,
        RoleRules roleRules
    )
    {
        _roleRepository = roleRepository;
        _roleRules = roleRules;
    }

    public async Task<IDataResult<DeletedRoleResponse>> Handle(
        DeleteRoleCommand request,
        CancellationToken cancellationToken
    )
    {
        Role? role = await _roleRepository.GetAsync(
            predicate: oc => oc.Id == request.Id,
            cancellationToken: cancellationToken
        );
        await _roleRules.RoleShouldExistWhenSelected(role);

        await _roleRepository.DeleteAsync(entity: role!);

        DeletedRoleResponse response = role.Adapt<DeletedRoleResponse>();
        var result = new SuccessDataResult<DeletedRoleResponse>(response);
        return result;
    }
}
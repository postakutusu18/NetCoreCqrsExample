using Application.Features.Roles.Rules;
using Application.Repositories.Users;
using Core.Application.Results;
using Domains.Users;
using Mapster;
using MediatR;

namespace Application.Features.Roles.Queries.GetById;

public class GetByIdRoleQueryHandler : IRequestHandler<GetByIdRoleQuery, IDataResult<GetByIdRoleResponse>>
{
    private readonly IRoleRepository _roleRepository;
    private readonly RoleRules _roleRules;

    public GetByIdRoleQueryHandler(
        IRoleRepository roleRepository,
        RoleRules roleRules
    )
    {
        _roleRepository = roleRepository;
        _roleRules = roleRules;
    }

    public async Task<IDataResult<GetByIdRoleResponse>> Handle(
        GetByIdRoleQuery request,
        CancellationToken cancellationToken
    )
    {
        Role? role = await _roleRepository.GetAsync(
            predicate: b => b.Id == request.Id,
            cancellationToken: cancellationToken,
            enableTracking: false
        );
        await _roleRules.RoleShouldExistWhenSelected(role);

        GetByIdRoleResponse response = role.Adapt<GetByIdRoleResponse>();
        var result = new SuccessDataResult<GetByIdRoleResponse>(response);
        return result;
    }
}
using Application.Features.UserFeatures.Roles.Rules;

namespace Application.Features.UserFeatures.Roles.Commands.Update;

public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, IDataResult<UpdatedRoleResponse>>
{
    private readonly IUnitOfWorkAsync _unitOfWorkAsync;
    private readonly RoleRules _roleRules;

    public UpdateRoleCommandHandler(
    IUnitOfWorkAsync unitOfWorkAsync,
    RoleRules roleRules
    )
    {
        _unitOfWorkAsync = unitOfWorkAsync;
        _roleRules = roleRules;
    }

    public async Task<IDataResult<UpdatedRoleResponse>> Handle(
        UpdateRoleCommand request,
        CancellationToken cancellationToken
    )
    {
        Role? role = await _unitOfWorkAsync.RoleRepository.GetAsync(
            predicate: oc => oc.Id == request.Id,
            cancellationToken: cancellationToken
        );
        await _roleRules.RoleShouldExistWhenSelected(role);
        await _roleRules.RoleNameShouldNotExistWhenUpdating(request.Id, request.Name);
        Role mappedRole = request.Adapt<Role>();

        Role updatedRole = await _unitOfWorkAsync.RoleRepository.UpdateAsync(mappedRole);
        await _unitOfWorkAsync.SaveAsync();

        UpdatedRoleResponse response = updatedRole.Adapt<UpdatedRoleResponse>();
        var result = new SuccessDataResult<UpdatedRoleResponse>(response);
        return result;
    }
}
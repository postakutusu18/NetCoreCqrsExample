using Application.Features.UserFeatures.Roles.Rules;
using Application.Features.UserFeatures.Roles.Constants;

namespace Application.Features.Users.Roles.Commands;

public class DeleteRoleHandler : IRequestHandler<DeleteRoleCommand, IDataResult<DeletedRoleResponse>>
{
    private readonly IUnitOfWorkAsync _unitOfWorkAsync;
    private readonly RoleRules _roleRules;

    public DeleteRoleHandler(
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
public record DeleteRoleCommand(int Id) : IRequest<IDataResult<DeletedRoleResponse>>, ISecuredRequest
{
    public string[] Roles => new[] { RoleOperationClaims.AdminRole, RoleOperationClaims.WriteRole, RoleOperationClaims.DeleteRole };
}
public record DeletedRoleResponse(int Id) : IResponse;

using Application.Features.UserFeatures.UserRoles.Constants;
using Application.Features.UserFeatures.UserRoles.Rules;

namespace Application.Features.Users.UserRoles.Commands;

public class DeleteUserRole
     : IRequestHandler<DeleteUserRoleCommand, IDataResult<DeletedUserRoleResponse>>
{
    private readonly IUnitOfWorkAsync _unitOfWorkAsync;
    private readonly UserRoleRules _userRoleRules;

    public DeleteUserRole(
        IUnitOfWorkAsync unitOfWorkAsync,
        UserRoleRules userRoleRules
    )
    {
        _unitOfWorkAsync = unitOfWorkAsync;
        _userRoleRules = userRoleRules;
    }

    public async Task<IDataResult<DeletedUserRoleResponse>> Handle(
        DeleteUserRoleCommand request,
        CancellationToken cancellationToken
    )
    {
        UserRole? userRole = await _unitOfWorkAsync.UserRoleRepository.GetAsync(
            predicate: uoc => uoc.Id.Equals(request.Id),
            cancellationToken: cancellationToken
        );
        await _userRoleRules.UserRoleShouldExistWhenSelected(userRole);

        await _unitOfWorkAsync.UserRoleRepository.DeleteAsync(userRole!);
        await _unitOfWorkAsync.SaveAsync();

        DeletedUserRoleResponse response = userRole.Adapt<DeletedUserRoleResponse>();
        var result = new SuccessDataResult<DeletedUserRoleResponse>(response);
        return result;
    }
}
public record DeleteUserRoleCommand(Guid Id) : IRequest<IDataResult<DeletedUserRoleResponse>>, ISecuredRequest
{
    public string[] Roles => new[] { UserRoleOperationClaims.AdminRole, UserRoleOperationClaims.WriteRole, UserRoleOperationClaims.DeleteRole };
}
public record DeletedUserRoleResponse(Guid Id) : IResponse;

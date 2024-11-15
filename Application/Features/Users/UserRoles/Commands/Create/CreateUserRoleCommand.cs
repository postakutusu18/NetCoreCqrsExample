using Application.Features.UserFeatures.UserRoles.Constants;

namespace Application.Features.UserFeatures.UserRoles.Commands.Create;

public partial class CreateUserRoleCommand : IRequest<IDataResult<CreatedUserRoleResponse>>, ISecuredRequest
{
    public Guid UserId { get; set; }
    public int RoleId { get; set; }

    public string[] Roles => new[] { UserRoleOperationClaims.AdminRole, UserRoleOperationClaims.WriteRole, UserRoleOperationClaims.CreateRole };
}

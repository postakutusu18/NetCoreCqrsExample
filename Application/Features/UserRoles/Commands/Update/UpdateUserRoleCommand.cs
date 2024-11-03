using Application.Features.UserRoles.Constants;
using Core.Application.Pipelines.Authorization;
using Core.Application.Results;
using MediatR;

namespace Application.Features.UserRoles.Commands.Update;

public partial class UpdateUserRoleCommand : IRequest<IDataResult<UpdatedUserRoleResponse>>, ISecuredRequest
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public int RoleId { get; set; }

    public string[] Roles => new[] {UserRoleOperationClaims.AdminRole, UserRoleOperationClaims.WriteRole, UserRoleOperationClaims.DeleteRole };
}
using Application.Features.UserRoles.Constants;
using Core.Application.Pipelines.Authorization;
using Core.Application.Results;
using MediatR;

namespace Application.Features.UserRoles.Commands.Create;

public partial class CreateUserRoleCommand : IRequest<IDataResult<CreatedUserRoleResponse>>, ISecuredRequest
{
    public Guid UserId { get; set; }
    public int RoleId { get; set; }

    public string[] Roles => new[] { UserRoleOperationClaims.AdminRole, UserRoleOperationClaims.WriteRole, UserRoleOperationClaims.CreateRole  };
}

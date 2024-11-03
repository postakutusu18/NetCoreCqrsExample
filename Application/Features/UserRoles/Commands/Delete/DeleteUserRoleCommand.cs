using Application.Features.UserRoles.Constants;
using Core.Application.Pipelines.Authorization;
using Core.Application.Results;
using MediatR;

namespace Application.Features.UserRoles.Commands.Delete;

public partial class DeleteUserRoleCommand : IRequest<IDataResult<DeletedUserRoleResponse>>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { UserRoleOperationClaims.AdminRole, UserRoleOperationClaims.WriteRole, UserRoleOperationClaims.DeleteRole };
}
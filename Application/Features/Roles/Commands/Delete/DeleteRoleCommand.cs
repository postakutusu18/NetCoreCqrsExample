using Core.Application.Pipelines.Authorization;
using Core.Application.Results;
using MediatR;
using static Application.Features.Roles.Constants.RoleOperationClaims;


namespace Application.Features.Roles.Commands.Delete;

public class DeleteRoleCommand : IRequest<IDataResult<DeletedRoleResponse>>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { AdminRole, WriteRole, DeleteRole }; 
}
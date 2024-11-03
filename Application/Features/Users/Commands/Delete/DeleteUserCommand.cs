using Application.Features.Users.Constants;
using Core.Application.Pipelines.Authorization;
using Core.Application.Results;
using MediatR;

namespace Application.Features.Users.Commands.Delete;

public class DeleteUserCommand : IRequest<IDataResult<DeletedUserResponse>>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { UsersOperationClaims.AdminRole, UsersOperationClaims.WriteRole, UsersOperationClaims.DeleteRole };


}

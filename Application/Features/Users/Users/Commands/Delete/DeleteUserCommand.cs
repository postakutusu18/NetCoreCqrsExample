using Application.Features.UserFeatures.Users.Constants;
using Core.Application.Pipelines.Authorization;
using Core.Application.Results;
using MediatR;

namespace Application.Features.UserFeatures.Users.Commands.Delete;

public class DeleteUserCommand : IRequest<IDataResult<DeletedUserResponse>>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { UsersOperationClaims.AdminRole, UsersOperationClaims.WriteRole, UsersOperationClaims.DeleteRole };


}

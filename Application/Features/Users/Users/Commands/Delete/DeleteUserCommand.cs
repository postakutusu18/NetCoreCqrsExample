using Application.Features.UserFeatures.Users.Constants;

namespace Application.Features.UserFeatures.Users.Commands.Delete;

public class DeleteUserCommand : IRequest<IDataResult<DeletedUserResponse>>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { UsersOperationClaims.AdminRole, UsersOperationClaims.WriteRole, UsersOperationClaims.DeleteRole };


}

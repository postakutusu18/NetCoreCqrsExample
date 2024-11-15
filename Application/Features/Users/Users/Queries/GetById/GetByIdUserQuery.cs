using Application.Features.UserFeatures.Users.Constants;
using Application.Features.UserFeatures.Users.Queries.GetById;

namespace Application.Features.Users.Queries.GetById;

public partial class GetByIdUserQuery : IRequest<IDataResult<GetByIdUserResponse>>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [UsersOperationClaims.ReadRole];
}

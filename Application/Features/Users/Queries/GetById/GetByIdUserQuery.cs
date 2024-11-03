using Application.Features.Users.Constants;
using Core.Application.Pipelines.Authorization;
using Core.Application.Results;
using MediatR;

namespace Application.Features.Users.Queries.GetById;

public partial class GetByIdUserQuery : IRequest<IDataResult<GetByIdUserResponse>>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [UsersOperationClaims.ReadRole];
}

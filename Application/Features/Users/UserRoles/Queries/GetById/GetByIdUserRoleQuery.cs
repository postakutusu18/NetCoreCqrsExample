using Application.Features.UserFeatures.UserRoles.Constants;
using Application.Features.UserFeatures.UserRoles.Queries.GetById;
using Core.Application.Pipelines.Authorization;
using Core.Application.Results;
using MediatR;

namespace Application.Features.UserRoles.Queries.GetById;

public partial class GetByIdUserRoleQuery : IRequest<IDataResult<GetByIdUserRoleResponse>>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [UserRoleOperationClaims.ReadRole];
}

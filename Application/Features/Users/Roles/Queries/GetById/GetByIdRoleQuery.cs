using Application.Features.UserFeatures.Roles.Constants;
using Core.Application.Pipelines.Authorization;
using Core.Application.Results;
using MediatR;

namespace Application.Features.UserFeatures.Roles.Queries.GetById;


public class GetByIdRoleQuery : IRequest<IDataResult<GetByIdRoleResponse>>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => [RoleOperationClaims.ReadRole];


}

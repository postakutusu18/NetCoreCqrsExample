using Application.Features.UserRoles.Constants;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Application.Results;
using MediatR;

namespace Application.Features.UserRoles.Queries.GetList;

public partial class GetListUserRoleQuery : IRequest<IDataResult<GetListResponse<GetListUserRoleListResponse>>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [UserRoleOperationClaims.ReadRole];

    public GetListUserRoleQuery()
    {
        PageRequest = new PageRequest { PageIndex = 0, PageSize = 10 };
    }

    public GetListUserRoleQuery(PageRequest pageRequest)
    {
        PageRequest = pageRequest;
    }
}

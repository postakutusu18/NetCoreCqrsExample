using Application.Features.Roles.Constants;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Application.Results;
using MediatR;
namespace Application.Features.Roles.Queries.GetList;

public class GetListRoleQuery : IRequest<IDataResult<GetListResponse<GetListRoleResponse>>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [RoleOperationClaims.ReadRole];

    public GetListRoleQuery()
    {
        PageRequest = new PageRequest { PageIndex = 0, PageSize = 10 };
    }

    public GetListRoleQuery(PageRequest pageRequest)
    {
        PageRequest = pageRequest;
    }

    
}

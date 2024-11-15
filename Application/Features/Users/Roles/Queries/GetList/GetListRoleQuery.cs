using Application.Features.UserFeatures.Roles.Constants;
namespace Application.Features.UserFeatures.Roles.Queries.GetList;

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

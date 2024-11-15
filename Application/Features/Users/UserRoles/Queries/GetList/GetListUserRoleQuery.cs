using Application.Features.UserFeatures.UserRoles.Constants;
using Application.Features.UserFeatures.UserRoles.Queries.GetList;

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

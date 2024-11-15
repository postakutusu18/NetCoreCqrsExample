using Application.Features.UserFeatures.Users.Constants;
using Application.Features.UserFeatures.Users.Queries.GetList;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Application.Results;
using MediatR;

namespace Application.Features.Users.Queries.GetList;

public partial class GetListUserQuery : IRequest<IDataResult<GetListResponse<GetListUserListResponse>>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [UsersOperationClaims.ReadRole];

    public GetListUserQuery()
    {
        PageRequest = new PageRequest { PageIndex = 0, PageSize = 10 };
    }

    public GetListUserQuery(PageRequest pageRequest)
    {
        PageRequest = pageRequest;
    }
}

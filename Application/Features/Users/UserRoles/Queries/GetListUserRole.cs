using Application.Features.UserFeatures.UserRoles.Constants;
using Core.Application.Dtos;

namespace Application.Features.UserRoles.Queries.GetList;

    public class GetListUserRole
        : IRequestHandler<GetListUserRoleQuery,IDataResult<GetListResponse<GetListUserRoleListResponse>>>
    {
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public GetListUserRole(IUnitOfWorkAsync unitOfWorkAsync)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        public async Task<IDataResult<GetListResponse<GetListUserRoleListResponse>>> Handle(
            GetListUserRoleQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<UserRole> userRoles = await _unitOfWorkAsync.UserRoleRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                enableTracking: false
            );

            var mappedUserRoleListModel = userRoles.Adapt<GetListResponse<GetListUserRoleListResponse>>();
            var result = new SuccessDataResult<GetListResponse<GetListUserRoleListResponse>>( mappedUserRoleListModel );
            return result;
        }
    }

public record GetListUserRoleQuery(PageRequest PageRequest) : IRequest<IDataResult<GetListResponse<GetListUserRoleListResponse>>>, ISecuredRequest
{
    public string[] Roles => new[] { UserRoleOperationClaims.ReadRole };

    public GetListUserRoleQuery() : this(new PageRequest { PageIndex = 0, PageSize = 10 }) { }
}
public record GetListUserRoleListResponse(Guid Id, Guid UserId, int RoleId) : IDto;

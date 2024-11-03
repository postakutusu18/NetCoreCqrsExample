using Application.Repositories.Users;
using Core.Application.Responses;
using Core.Application.Results;
using Core.Persistance.Paging;
using Domains.Users;
using Mapster;
using MediatR;

namespace Application.Features.UserRoles.Queries.GetList;

public partial class GetListUserRoleQuery
{
    public class GetListUserRoleQueryHandler
        : IRequestHandler<GetListUserRoleQuery,IDataResult<GetListResponse<GetListUserRoleListResponse>>>
    {
        private readonly IUserRoleRepository _userRoleRepository;

        public GetListUserRoleQueryHandler(IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }

        public async Task<IDataResult<GetListResponse<GetListUserRoleListResponse>>> Handle(
            GetListUserRoleQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<UserRole> userRoles = await _userRoleRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                enableTracking: false
            );

            var mappedUserRoleListModel = userRoles.Adapt<GetListResponse<GetListUserRoleListResponse>>();
            var result = new SuccessDataResult<GetListResponse<GetListUserRoleListResponse>>( mappedUserRoleListModel );
            return result;
        }
    }
}

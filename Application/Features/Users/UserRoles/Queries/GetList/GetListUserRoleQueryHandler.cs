using Application.Features.UserFeatures.UserRoles.Queries.GetList;
using Application.Repositories;
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
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public GetListUserRoleQueryHandler(IUnitOfWorkAsync unitOfWorkAsync)
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
}

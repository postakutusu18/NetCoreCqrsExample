using Application.Features.UserFeatures.Users.Queries.GetList;

namespace Application.Features.Users.Queries.GetList;

public partial class GetListUserQuery
{
    public class GetListUserQueryHandler : IRequestHandler<GetListUserQuery,IDataResult<GetListResponse<GetListUserListResponse>>>
    {
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public GetListUserQueryHandler(IUnitOfWorkAsync unitOfWorkAsync)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        public async Task<IDataResult<GetListResponse<GetListUserListResponse>>> Handle(
            GetListUserQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<User> users = await _unitOfWorkAsync.UserRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                enableTracking: false,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListUserListResponse> response = users.Adapt<GetListResponse<GetListUserListResponse>>();
            var result = new SuccessDataResult<GetListResponse<GetListUserListResponse>>(response);
            return result;
        }
    }
}

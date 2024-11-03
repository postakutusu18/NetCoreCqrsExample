using Application.Repositories.Users;
using Core.Application.Responses;
using Core.Application.Results;
using Core.Persistance.Paging;
using Domains.Users;
using Mapster;
using MediatR;

namespace Application.Features.Users.Queries.GetList;

public partial class GetListUserQuery
{
    public class GetListUserQueryHandler : IRequestHandler<GetListUserQuery,IDataResult<GetListResponse<GetListUserListResponse>>>
    {
        private readonly IUserRepository _userRepository;

        public GetListUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IDataResult<GetListResponse<GetListUserListResponse>>> Handle(
            GetListUserQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<User> users = await _userRepository.GetListAsync(
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

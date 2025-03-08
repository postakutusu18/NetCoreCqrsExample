using Application.Features.UserFeatures.Users.Constants;
using Core.Application.Dtos;

namespace Application.Features.Users.Queries.GetList;

    public class GetListUser : IRequestHandler<GetListUserQuery,IDataResult<GetListResponse<GetListUserListResponse>>>
    {
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public GetListUser(IUnitOfWorkAsync unitOfWorkAsync)
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
public class GetListUserListResponse : IDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public bool Status { get; set; }

    public GetListUserListResponse()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        Email = string.Empty;
    }

    public GetListUserListResponse(Guid id, string firstName, string lastName, string email, bool status)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Status = status;
    }
}

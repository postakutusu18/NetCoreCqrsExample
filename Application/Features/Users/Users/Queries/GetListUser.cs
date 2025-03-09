using Application.Features.UserFeatures.Users.Constants;

namespace Application.Features.Users.Queries.GetList;

public class GetListUser : IRequestHandler<GetListUserQuery,IDataResult<GetListResponse<GetListUserListResponse>>>
    {
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
    private readonly ILocalizationService _localizationService;

    public GetListUser(IUnitOfWorkAsync unitOfWorkAsync, ILocalizationService localizationService)
    {
        _unitOfWorkAsync = unitOfWorkAsync;
        _localizationService = localizationService;
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
        string message = await _localizationService.GetLocalizedAsync(UsersMessages.SuccessList, UsersMessages.SectionName);
        var result = new SuccessDataResult<GetListResponse<GetListUserListResponse>>(response, message);
            return result;
        }
    }
public partial class GetListUserQuery : IRequest<IDataResult<GetListResponse<GetListUserListResponse>>>//, ISecuredRequest
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
public record GetListUserListResponse(Guid Id, string FirstName, string LastName, string Email, bool Status);

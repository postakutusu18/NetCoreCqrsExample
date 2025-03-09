using Application.Features.UserFeatures.Roles.Constants;

namespace Application.Features.Users.Roles.Queries;

public class GetListRole
        : IRequestHandler<GetListRoleQuery, IDataResult<GetListResponse<GetListRoleResponse>>>
{
    private readonly IUnitOfWorkAsync _unitOfWorkAsync;
    private readonly ILocalizationService _localizationService;

    public GetListRole(IUnitOfWorkAsync unitOfWorkAsync, ILocalizationService localizationService)
    {
        _unitOfWorkAsync = unitOfWorkAsync;
        _localizationService = localizationService;
    }

    public async Task<IDataResult<GetListResponse<GetListRoleResponse>>> Handle(
        GetListRoleQuery request,
        CancellationToken cancellationToken
    )
    {
        IPaginate<Role> roles = await _unitOfWorkAsync.RoleRepository.GetListAsync(
            index: request.PageRequest.PageIndex,
            size: request.PageRequest.PageSize,
            enableTracking: false,
            cancellationToken: cancellationToken
        );

        var response = roles.Adapt<GetListResponse<GetListRoleResponse>>();
        string message = await _localizationService.GetLocalizedAsync(RoleMessages.SuccessList, RoleMessages.SectionName);
        var result = new SuccessDataResult<GetListResponse<GetListRoleResponse>>(response, message);
        return result;
    }
}

public class GetListRoleQuery : IRequest<IDataResult<GetListResponse<GetListRoleResponse>>>//, ISecuredRequest
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
public record GetListRoleResponse(int Id, string Name);

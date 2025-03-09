using Application.Features.UserFeatures.UserRoles.Constants;
using Core.Application.Dtos;

namespace Application.Features.UserRoles.Queries.GetList;

public class GetListUserRole
        : IRequestHandler<GetListUserRoleQuery,IDataResult<GetListResponse<GetListUserRoleListResponse>>>
    {
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
    private readonly ILocalizationService _localizationService;

    public GetListUserRole(IUnitOfWorkAsync unitOfWorkAsync, ILocalizationService localizationService)
    {
        _unitOfWorkAsync = unitOfWorkAsync;
        _localizationService = localizationService;
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
        string message = await _localizationService.GetLocalizedAsync(UserRolesMessages.SuccessList, UserRolesMessages.SectionName);
        var result = new SuccessDataResult<GetListResponse<GetListUserRoleListResponse>>( mappedUserRoleListModel,message);
            return result;
        }
    }

public record GetListUserRoleQuery(PageRequest PageRequest) : IRequest<IDataResult<GetListResponse<GetListUserRoleListResponse>>>//, ISecuredRequest
{
    public string[] Roles => new[] { UserRoleOperationClaims.ReadRole };

    public GetListUserRoleQuery() : this(new PageRequest { PageIndex = 0, PageSize = 10 }) { }
}
public record GetListUserRoleListResponse(Guid Id, Guid UserId, int RoleId) : IDto;

using Application.Features.UserFeatures.UserRoles.Constants;
using Application.Features.UserFeatures.UserRoles.Rules;

namespace Application.Features.UserRoles.Queries.GetById;

public class GetByIdUserRole
        : IRequestHandler<GetByIdUserRoleQuery,IDataResult<GetByIdUserRoleResponse>>
    {
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly UserRoleRules _userRoleRules;
    private readonly ILocalizationService _localizationService;

    public GetByIdUserRole(
        IUnitOfWorkAsync unitOfWorkAsync,
            UserRoleRules userRoleRules
,
            ILocalizationService localizationService)
    {
        _unitOfWorkAsync = unitOfWorkAsync;
        _userRoleRules = userRoleRules;
        _localizationService = localizationService;
    }

    public async Task<IDataResult<GetByIdUserRoleResponse>> Handle(
            GetByIdUserRoleQuery request,
            CancellationToken cancellationToken
        )
        {
            UserRole? userRole = await _unitOfWorkAsync.UserRoleRepository.GetAsync(
                predicate: b => b.Id.Equals(request.Id),
                enableTracking: false,
                cancellationToken: cancellationToken
            );
            await _userRoleRules.UserRoleShouldExistWhenSelected(userRole);

            GetByIdUserRoleResponse userRoleDto = userRole.Adapt<GetByIdUserRoleResponse>();
        string message = await _localizationService.GetLocalizedAsync(UserRolesMessages.SuccessRecord, UserRolesMessages.SectionName);
        var result = new SuccessDataResult<GetByIdUserRoleResponse>(userRoleDto, message);
            return result;
        }
    }

public record GetByIdUserRoleQuery(Guid Id) : IRequest<IDataResult<GetByIdUserRoleResponse>>//, ISecuredRequest
{
    public string[] Roles => new[] { UserRoleOperationClaims.ReadRole };
}

public record GetByIdUserRoleResponse(Guid Id, Guid UserId, int RoleId) : IResponse;

using Application.Features.UserFeatures.UserRoles.Constants;
using Application.Features.UserFeatures.UserRoles.Rules;

namespace Application.Features.UserRoles.Commands.Update;


public class UpdateUserRole
        : IRequestHandler<UpdateUserRoleCommand,IDataResult<UpdatedUserRoleResponse>>
    {
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly UserRoleRules _userRoleRules;
    private readonly ILocalizationService _localizationService;

    public UpdateUserRole(
        IUnitOfWorkAsync unitOfWorkAsync,
            UserRoleRules userRoleRules
,
            ILocalizationService localizationService)
    {
        _unitOfWorkAsync = unitOfWorkAsync;
        _userRoleRules = userRoleRules;
        _localizationService = localizationService;
    }

    public async Task<IDataResult<UpdatedUserRoleResponse>> Handle(
            UpdateUserRoleCommand request,
            CancellationToken cancellationToken
        )
        {
            UserRole? userRole = await _unitOfWorkAsync.UserRoleRepository.GetAsync(
                predicate: uoc => uoc.Id.Equals(request.Id),
                enableTracking: false,
                cancellationToken: cancellationToken
            );
            await _userRoleRules.UserRoleShouldExistWhenSelected(userRole);
            await _userRoleRules.UserShouldNotHasRoleAlreadyWhenUpdated(
                request.Id,
                request.UserId,
                request.RoleId
            );
            var mappedUserRole = request.Adapt(userRole);
            UserRole updatedUserRole = await _unitOfWorkAsync.UserRoleRepository.UpdateAsync(
                mappedUserRole
            );
            await _unitOfWorkAsync.SaveAsync();

            var updatedUserRoleDto = updatedUserRole.Adapt<UpdatedUserRoleResponse>();
        string message = await _localizationService.GetLocalizedAsync(UserRolesMessages.SuccessUpdated, UserRolesMessages.SectionName);
        var result = new SuccessDataResult<UpdatedUserRoleResponse>(updatedUserRoleDto,message);
            return result;
        }
    }

public record UpdateUserRoleCommand(Guid Id, Guid UserId, int RoleId) : IRequest<IDataResult<UpdatedUserRoleResponse>>//, ISecuredRequest
{
    public string[] Roles => new[] { UserRoleOperationClaims.AdminRole, UserRoleOperationClaims.WriteRole, UserRoleOperationClaims.DeleteRole };
}
public record UpdatedUserRoleResponse(Guid Id, Guid UserId, int RoleId) : IResponse;

using Application.Features.UserFeatures.UserRoles.Constants;
using Application.Features.UserFeatures.UserRoles.Rules;

namespace Application.Features.Users.UserRoles.Commands;
public class CreateUserRole
       : IRequestHandler<CreateUserRoleCommand, IDataResult<CreatedUserRoleResponse>>
{
    private readonly IUnitOfWorkAsync _unitOfWorkAsync;
    private readonly UserRoleRules _userRoleRules;
    private readonly ILocalizationService _localizationService;

    public CreateUserRole(
        IUnitOfWorkAsync unitOfWorkAsync,
        UserRoleRules userRoleRules
,
        ILocalizationService localizationService)
    {
        _unitOfWorkAsync = unitOfWorkAsync;
        _userRoleRules = userRoleRules;
        _localizationService = localizationService;
    }

    public async Task<IDataResult<CreatedUserRoleResponse>> Handle(
        CreateUserRoleCommand request,
        CancellationToken cancellationToken
    )
    {
        await _userRoleRules.UserShouldNotHasRoleAlreadyWhenInsert(
            request.UserId,
            request.RoleId
        );
        UserRole mappedUserRole = request.Adapt<UserRole>();

        UserRole createdUserRole = await _unitOfWorkAsync.UserRoleRepository.AddAsync(mappedUserRole);
        await _unitOfWorkAsync.SaveAsync();

        var createdUserRoleDto = createdUserRole.Adapt<CreatedUserRoleResponse>();
        string message = await _localizationService.GetLocalizedAsync(UserRolesMessages.SuccessInserted, UserRolesMessages.SectionName);
        var result = new SuccessDataResult<CreatedUserRoleResponse>(createdUserRoleDto,message);
        return result;
    }
}


public record CreateUserRoleCommand(Guid UserId, int RoleId) : IRequest<IDataResult<CreatedUserRoleResponse>>//, ISecuredRequest
{
    public string[] Roles => new[] { UserRoleOperationClaims.AdminRole, UserRoleOperationClaims.WriteRole, UserRoleOperationClaims.CreateRole };
}
public record CreatedUserRoleResponse(Guid Id, Guid UserId, int RoleId) : IResponse;

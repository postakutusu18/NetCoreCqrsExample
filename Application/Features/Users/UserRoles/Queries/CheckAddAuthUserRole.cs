using Application.Features.UserFeatures.UserRoles.Constants;

namespace Application.Features.Users.UserRoles.Queries;

public class CheckAddAuthUserRole : IRequestHandler<CheckAddAuthUserRoleQuery,IResult>
{
    private readonly ILocalizationService _localizationService;
    public CheckAddAuthUserRole(ILocalizationService localizationService)
    {
        _localizationService = localizationService;
    }
    public async Task<IResult> Handle(CheckAddAuthUserRoleQuery request, CancellationToken cancellationToken)
    {
        string message = await _localizationService.GetLocalizedAsync(UserRolesMessages.AuthorizedUser, UserRolesMessages.SectionName);
        return new SuccessResult(message);
    }
}

public record CheckAddAuthUserRoleQuery : IRequest<IResult>, ISecuredRequest
{
    public string[] Roles => new[] { UserRoleOperationClaims.CreateRole };
}

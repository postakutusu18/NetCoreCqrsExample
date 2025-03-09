using Application.Features.UserFeatures.UserRoles.Constants;

namespace Application.Features.Users.UserRoles.Queries;

internal class CheckUpdateAuthUserRole : IRequestHandler<CheckUpdateAuthUserRoleQuery, IResult>
{
    private readonly ILocalizationService _localizationService;
    public CheckUpdateAuthUserRole(ILocalizationService localizationService)
    {
        _localizationService = localizationService;
    }
    public async Task<IResult> Handle(CheckUpdateAuthUserRoleQuery request, CancellationToken cancellationToken)
    {
        string message = await _localizationService.GetLocalizedAsync(UserRolesMessages.AuthorizedUser, UserRolesMessages.SectionName);
        return new SuccessResult(message);
    }
}

public record CheckUpdateAuthUserRoleQuery : IRequest<IResult>, ISecuredRequest
{
    public string[] Roles => new[] { UserRoleOperationClaims.UpdateRole };
}

using Application.Features.UserFeatures.Roles.Constants;

namespace Application.Features.Users.Roles.Queries;

public class CheckUpdateAuthRole : IRequestHandler<CheckUpdateAuthRoleQuery, IResult>
{
    private readonly ILocalizationService _localizationService;
    public CheckUpdateAuthRole(ILocalizationService localizationService)
    {
        _localizationService = localizationService;
    }
    public async Task<IResult> Handle(CheckUpdateAuthRoleQuery request, CancellationToken cancellationToken)
    {
        string message = await _localizationService.GetLocalizedAsync(RoleMessages.AuthorizedUser, RoleMessages.SectionName);
        return new SuccessResult(message);
    }
}

public record CheckUpdateAuthRoleQuery : IRequest<IResult>, ISecuredRequest
{
    public string[] Roles => new[] { RoleOperationClaims.UpdateRole };
}

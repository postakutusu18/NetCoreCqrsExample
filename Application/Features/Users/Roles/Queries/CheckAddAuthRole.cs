using Application.Features.UserFeatures.Roles.Constants;

namespace Application.Features.Users.Roles.Queries;

public class CheckAddAuthRole : IRequestHandler<CheckAddAuthRoleQuery, IResult>
{
    private readonly ILocalizationService _localizationService;
    public CheckAddAuthRole(ILocalizationService localizationService)
    {
        _localizationService = localizationService;
    }
    public async Task<IResult> Handle(CheckAddAuthRoleQuery request, CancellationToken cancellationToken)
    {
        string message = await _localizationService.GetLocalizedAsync(RoleMessages.AuthorizedUser, RoleMessages.SectionName);
        return new SuccessResult(message);
    }
}

public record CheckAddAuthRoleQuery : IRequest<IResult>, ISecuredRequest
{
    public string[] Roles => new[] { RoleOperationClaims.CreateRole };
}

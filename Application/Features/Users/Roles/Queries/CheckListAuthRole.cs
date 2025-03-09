using Application.Features.UserFeatures.Roles.Constants;

namespace Application.Features.Users.Roles.Queries;

public class CheckListAuthRole : IRequestHandler<CheckListAuthRoleQuery, IResult>
{
    private readonly ILocalizationService _localizationService;
    public CheckListAuthRole(ILocalizationService localizationService)
    {
        _localizationService = localizationService;
    }
    public async Task<IResult> Handle(CheckListAuthRoleQuery request, CancellationToken cancellationToken)
    {
        string message = await _localizationService.GetLocalizedAsync(RoleMessages.AuthorizedUser, RoleMessages.SectionName);
        return new SuccessResult(message);
    }
}

public record CheckListAuthRoleQuery : IRequest<IResult>, ISecuredRequest
{
    public string[] Roles => new[] { RoleOperationClaims.ReadRole };
}

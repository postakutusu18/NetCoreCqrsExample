using Application.Features.UserFeatures.UserRoles.Constants;

namespace Application.Features.Users.UserRoles.Queries;

public class CheckListAuthUserRole : IRequestHandler<CheckListAuthUserRoleQuery, IResult>
{
    private readonly ILocalizationService _localizationService;
    public CheckListAuthUserRole(ILocalizationService localizationService)
    {
        _localizationService = localizationService;
    }
    public async Task<IResult> Handle(CheckListAuthUserRoleQuery request, CancellationToken cancellationToken)
    {
        string message = await _localizationService.GetLocalizedAsync(UserRolesMessages.AuthorizedUser, ExampleEntiesMessages.SectionName);
        return new SuccessResult(message);
    }
}

public record CheckListAuthUserRoleQuery : IRequest<IResult>, ISecuredRequest
{
    public string[] Roles => new[] { UserRoleOperationClaims.ReadRole };
}

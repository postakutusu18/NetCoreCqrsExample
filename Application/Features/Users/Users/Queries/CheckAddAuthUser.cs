using Application.Features.UserFeatures.Users.Constants;

namespace Application.Features.Users.Users.Queries;

public class CheckAddAuthUser : IRequestHandler<CheckAddAuthUserQuery, IResult>
{
    private readonly ILocalizationService _localizationService;
    public CheckAddAuthUser(ILocalizationService localizationService)
    {
        _localizationService = localizationService;
    }
    public async Task<IResult> Handle(CheckAddAuthUserQuery request, CancellationToken cancellationToken)
    {
        string message = await _localizationService.GetLocalizedAsync(UsersMessages.AuthorizedUser, UsersMessages.SectionName);
        return new SuccessResult(message);
    }
}

public record CheckAddAuthUserQuery : IRequest<IResult>, ISecuredRequest
{
    public string[] Roles => new[] { UsersOperationClaims.CreateRole };
}

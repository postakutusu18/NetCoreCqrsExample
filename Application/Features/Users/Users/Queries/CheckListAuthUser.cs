using Application.Features.UserFeatures.Users.Constants;

namespace Application.Features.Users.Users.Queries;

public class CheckListAuthUser : IRequestHandler<CheckListAuthUserQuery, IResult>
{
    private readonly ILocalizationService _localizationService;
    public CheckListAuthUser(ILocalizationService localizationService)
    {
        _localizationService = localizationService;
    }
    public async Task<IResult> Handle(CheckListAuthUserQuery request, CancellationToken cancellationToken)
    {
        string message = await _localizationService.GetLocalizedAsync(UsersMessages.AuthorizedUser, UsersMessages.SectionName);
        return new SuccessResult(message);
    }
}

public record CheckListAuthUserQuery : IRequest<IResult>, ISecuredRequest
{
    public string[] Roles => new[] { UsersOperationClaims.ReadRole };
}

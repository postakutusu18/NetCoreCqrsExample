using Application.Features.UserFeatures.Users.Constants;

namespace Application.Features.Users.Users.Queries;

public class CheckUpdateAuthUser : IRequestHandler<CheckUpdateAuthUserQuery, IResult>
{
    private readonly ILocalizationService _localizationService;
    public CheckUpdateAuthUser(ILocalizationService localizationService)
    {
        _localizationService = localizationService;
    }
    public async Task<IResult> Handle(CheckUpdateAuthUserQuery request, CancellationToken cancellationToken)
    {
        string message = await _localizationService.GetLocalizedAsync(UsersMessages.AuthorizedUser, UsersMessages.SectionName);
        return new SuccessResult(message);
    }
}

public record CheckUpdateAuthUserQuery : IRequest<IResult>, ISecuredRequest
{
    public string[] Roles => new[] { UsersOperationClaims.UpdateRole };
}



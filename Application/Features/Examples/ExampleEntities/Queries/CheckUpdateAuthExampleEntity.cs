namespace Application.Features.Examples.ExampleEntities.Queries;

public class CheckUpdateAuthExampleEntity : IRequestHandler<AuthCheckUpdateExampleEntityQuery, IResult>
{
    private readonly ILocalizationService _localizationService;
    public CheckUpdateAuthExampleEntity(ILocalizationService localizationService)
    {
        _localizationService = localizationService;
    }
    public async Task<IResult> Handle(AuthCheckUpdateExampleEntityQuery request, CancellationToken cancellationToken)
    {
        string message = await _localizationService.GetLocalizedAsync(ExampleEntiesMessages.AuthorizedUser, ExampleEntiesMessages.SectionName);
        return new SuccessResult(message);
    }
}
public record AuthCheckUpdateExampleEntityQuery() : IRequest<IResult>, ISecuredRequest
{
    public string[] Roles => [ExampleEntiesOperationClaims.Update];
}

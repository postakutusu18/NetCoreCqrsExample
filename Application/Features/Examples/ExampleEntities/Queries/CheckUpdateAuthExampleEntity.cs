namespace Application.Features.Examples.ExampleEntities.Queries;

public class CheckUpdateAuthExampleEntity : IRequestHandler<CheckUpdateAuthExampleEntityQuery, IResult>
{
    private readonly ILocalizationService _localizationService;
    public CheckUpdateAuthExampleEntity(ILocalizationService localizationService)
    {
        _localizationService = localizationService;
    }
    public async Task<IResult> Handle(CheckUpdateAuthExampleEntityQuery request, CancellationToken cancellationToken)
    {
        string message = await _localizationService.GetLocalizedAsync(ExampleEntiesMessages.AuthorizedUser, ExampleEntiesMessages.SectionName);
        return new SuccessResult(message);
    }
}
public record CheckUpdateAuthExampleEntityQuery() : IRequest<IResult>, ISecuredRequest
{
    public string[] Roles => [ExampleEntiesOperationClaims.Update];
}

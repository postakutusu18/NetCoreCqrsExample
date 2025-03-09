namespace Application.Features.Examples.ExampleEntities.Queries;


public class CheckAddAuthExampleEntity : IRequestHandler<CheckAddAuthExampleEntityQuery, IResult>
{
    private readonly ILocalizationService _localizationService;

    public CheckAddAuthExampleEntity(ILocalizationService localizationService)
    {
        _localizationService = localizationService;
    }
    public async Task<IResult> Handle(CheckAddAuthExampleEntityQuery request, CancellationToken cancellationToken)
    {
        string message = await _localizationService.GetLocalizedAsync(ExampleEntiesMessages.AuthorizedUser, ExampleEntiesMessages.SectionName);
        return new SuccessResult(message);
    }
}

public record CheckAddAuthExampleEntityQuery : IRequest<IResult>, ISecuredRequest
{
    public string[] Roles => [ExampleEntiesOperationClaims.Add];
}

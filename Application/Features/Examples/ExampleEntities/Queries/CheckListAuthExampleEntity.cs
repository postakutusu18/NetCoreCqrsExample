namespace Application.Features.Examples.ExampleEntities.Queries;

public class CheckListAuthExampleEntity : IRequestHandler<CheckListAuthExampleEntityQuery, IResult>
{
    private readonly ILocalizationService _localizationService;
    public CheckListAuthExampleEntity(ILocalizationService localizationService)
    {
        _localizationService = localizationService;
    }
    public async Task<IResult> Handle(CheckListAuthExampleEntityQuery request, CancellationToken cancellationToken)
    {
        string message = await _localizationService.GetLocalizedAsync(ExampleEntiesMessages.AuthorizedUser, ExampleEntiesMessages.SectionName);
        return new SuccessResult(message);
    }
}

public record CheckListAuthExampleEntityQuery() : IRequest<IResult>, ISecuredRequest
{
    public string[] Roles => [ExampleEntiesOperationClaims.Admin, ExampleEntiesOperationClaims.Read];
}


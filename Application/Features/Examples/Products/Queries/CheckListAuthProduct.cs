namespace Application.Features.Examples.Products.Queries;

public class CheckListAuthProduct : IRequestHandler<CheckListAuthProductQuery, IResult>
{

    private readonly ILocalizationService _localizationService;

    public CheckListAuthProduct(ILocalizationService localizationService)
    {
        _localizationService = localizationService;
    }
    public async Task<IResult> Handle(CheckListAuthProductQuery request, CancellationToken cancellationToken)
    {
        string message = await _localizationService.GetLocalizedAsync(ProductsMessages.AuthorizedUser, ExampleEntiesMessages.SectionName);
        return new SuccessResult(message);
    }
}

public record CheckListAuthProductQuery() : IRequest<IResult>, ISecuredRequest
{
    public string[] Roles => [ProductsOperationClaims.Read];
}


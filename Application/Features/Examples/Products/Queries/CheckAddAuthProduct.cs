namespace Application.Features.Examples.Products.Queries;

public class CheckAddAuthProduct : IRequestHandler<CheckAddAuthProductQuery, IResult>
{
    private readonly ILocalizationService _localizationService;

    public CheckAddAuthProduct(ILocalizationService localizationService)
    {
        _localizationService = localizationService;
    }

    public async Task<IResult> Handle(CheckAddAuthProductQuery request, CancellationToken cancellationToken)
    {
        string message = await _localizationService.GetLocalizedAsync(ProductsMessages.AuthorizedUser, ExampleEntiesMessages.SectionName);
        return new SuccessResult(message);

    }
}

public record CheckAddAuthProductQuery : IRequest<IResult>, ISecuredRequest
{
    public string[] Roles => [ProductsOperationClaims.Add];
}
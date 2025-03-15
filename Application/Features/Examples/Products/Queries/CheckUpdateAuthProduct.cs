namespace Application.Features.Examples.Products.Queries;

public class CheckUpdateAuthProduct : IRequestHandler<CheckUpdateAuthProductQuery, IResult>
{
    private readonly ILocalizationService _localizationService;

    public CheckUpdateAuthProduct(ILocalizationService localizationService)
    {
        _localizationService = localizationService;
    }
   
    public async Task<IResult> Handle(CheckUpdateAuthProductQuery request, CancellationToken cancellationToken)
    {
        string message = await _localizationService.GetLocalizedAsync(ProductsMessages.AuthorizedUser, ExampleEntiesMessages.SectionName);
        return new SuccessResult(message);
    }
}
public record CheckUpdateAuthProductQuery() : IRequest<IResult>, ISecuredRequest
{
    public string[] Roles => [ProductsOperationClaims.Update];
}

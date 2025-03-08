
namespace Application.Features.Examples.Products.Queries.CheckAddAuth;

public record AuthCheckAddProductQuery : IRequest<IResult>, ISecuredRequest
{
    public string[] Roles => [ProductsOperationClaims.Add];
}
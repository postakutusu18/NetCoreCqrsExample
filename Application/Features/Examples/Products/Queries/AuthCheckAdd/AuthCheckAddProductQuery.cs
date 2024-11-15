
namespace Application.Features.Examples.Products.Queries.AuthCheckAdd;

public record AuthCheckAddProductQuery : IRequest<IResult>, ISecuredRequest
{
    public string[] Roles => [ProductsOperationClaims.Add];
}
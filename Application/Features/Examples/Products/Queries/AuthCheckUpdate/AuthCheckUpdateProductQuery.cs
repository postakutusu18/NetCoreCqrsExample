namespace Application.Features.Examples.Products.Queries.AuthCheckUpdate;

public record AuthCheckUpdateProductQuery() : IRequest<IResult>, ISecuredRequest
{
    public string[] Roles => [ProductsOperationClaims.Update];
}

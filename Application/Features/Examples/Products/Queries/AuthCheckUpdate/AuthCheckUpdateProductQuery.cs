namespace Application.Features.Examples.Products.Queries.CheckUpdateAuth;

public record AuthCheckUpdateProductQuery() : IRequest<IResult>, ISecuredRequest
{
    public string[] Roles => [ProductsOperationClaims.Update];
}

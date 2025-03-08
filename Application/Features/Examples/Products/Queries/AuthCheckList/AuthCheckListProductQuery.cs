namespace Application.Features.Examples.Products.Queries.CheckListAuth;

public record AuthCheckListProductQuery() : IRequest<IResult>, ISecuredRequest
{
    public string[] Roles => [ProductsOperationClaims.Read];
}


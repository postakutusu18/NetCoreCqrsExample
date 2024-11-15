namespace Application.Features.Examples.Products.Queries.AuthCheckList;

public record AuthCheckListProductQuery() : IRequest<IResult>, ISecuredRequest
{
    public string[] Roles => [ProductsOperationClaims.Read];
}


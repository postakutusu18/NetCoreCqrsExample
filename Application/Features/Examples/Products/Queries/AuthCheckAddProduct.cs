namespace Application.Features.Examples.Products.Queries;

public class AuthCheckAddProduct : IRequestHandler<AuthCheckAddProductQuery, IResult>
{
    public Task<IResult> Handle(AuthCheckAddProductQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult<IResult>(new SuccessResult());
    }
}

public record AuthCheckAddProductQuery : IRequest<IResult>, ISecuredRequest
{
    public string[] Roles => [ProductsOperationClaims.Add];
}
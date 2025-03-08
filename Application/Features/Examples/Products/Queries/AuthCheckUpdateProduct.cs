namespace Application.Features.Examples.Products.Queries;

public class AuthCheckUpdateProduct : IRequestHandler<AuthCheckUpdateProductQuery, IResult>
{
    public Task<IResult> Handle(AuthCheckUpdateProductQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult<IResult>(new SuccessResult());
    }
}
public record AuthCheckUpdateProductQuery() : IRequest<IResult>, ISecuredRequest
{
    public string[] Roles => [ProductsOperationClaims.Update];
}

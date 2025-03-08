namespace Application.Features.Examples.Products.Queries;

public class AuthCheckListProduct : IRequestHandler<AuthCheckListProductQuery, IResult>
{
    public Task<IResult> Handle(AuthCheckListProductQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult<IResult>(new SuccessResult());
    }
}

public record AuthCheckListProductQuery() : IRequest<IResult>, ISecuredRequest
{
    public string[] Roles => [ProductsOperationClaims.Read];
}


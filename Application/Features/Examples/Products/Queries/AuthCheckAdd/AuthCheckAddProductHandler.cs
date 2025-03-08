namespace Application.Features.Examples.Products.Queries.CheckAddAuth;

public class AuthCheckAddProductHandler : IRequestHandler<AuthCheckAddProductQuery, IResult>
{
    public Task<IResult> Handle(AuthCheckAddProductQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult<IResult>(new SuccessResult());
    }
}
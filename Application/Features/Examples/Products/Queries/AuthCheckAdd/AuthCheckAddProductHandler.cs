namespace Application.Features.Examples.Products.Queries.AuthCheckAdd;

public class AuthCheckAddProductHandler : IRequestHandler<AuthCheckAddProductQuery, IResult>
{
    public Task<IResult> Handle(AuthCheckAddProductQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult<IResult>(new SuccessResult());
    }
}
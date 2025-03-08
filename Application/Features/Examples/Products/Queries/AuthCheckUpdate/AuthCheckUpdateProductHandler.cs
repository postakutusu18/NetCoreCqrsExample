namespace Application.Features.Examples.Products.Queries.CheckUpdateAuth;

public class AuthCheckUpdateProductHandler : IRequestHandler<AuthCheckUpdateProductQuery, IResult>
{
    public Task<IResult> Handle(AuthCheckUpdateProductQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult<IResult>(new SuccessResult());
    }
}
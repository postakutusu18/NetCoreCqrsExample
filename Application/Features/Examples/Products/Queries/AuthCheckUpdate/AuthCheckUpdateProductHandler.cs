namespace Application.Features.Examples.Products.Queries.AuthCheckUpdate;

public class AuthCheckUpdateProductHandler : IRequestHandler<AuthCheckUpdateProductQuery, IResult>
{
    public Task<IResult> Handle(AuthCheckUpdateProductQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult<IResult>(new SuccessResult());
    }
}
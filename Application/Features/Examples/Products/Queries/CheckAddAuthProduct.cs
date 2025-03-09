namespace Application.Features.Examples.Products.Queries;

public class CheckAddAuthProduct : IRequestHandler<CheckAddAuthProductQuery, IResult>
{
    public Task<IResult> Handle(CheckAddAuthProductQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult<IResult>(new SuccessResult());
    }
}

public record CheckAddAuthProductQuery : IRequest<IResult>, ISecuredRequest
{
    public string[] Roles => [ProductsOperationClaims.Add];
}
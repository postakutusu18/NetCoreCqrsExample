namespace Application.Features.Examples.Products.Queries;

public class CheckUpdateAuthProduct : IRequestHandler<CheckUpdateAuthProductQuery, IResult>
{
    public Task<IResult> Handle(CheckUpdateAuthProductQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult<IResult>(new SuccessResult());
    }
}
public record CheckUpdateAuthProductQuery() : IRequest<IResult>, ISecuredRequest
{
    public string[] Roles => [ProductsOperationClaims.Update];
}

namespace Application.Features.Examples.Products.Queries;

public class CheckListAuthProduct : IRequestHandler<CheckListAuthProductQuery, IResult>
{
    public Task<IResult> Handle(CheckListAuthProductQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult<IResult>(new SuccessResult());
    }
}

public record CheckListAuthProductQuery() : IRequest<IResult>, ISecuredRequest
{
    public string[] Roles => [ProductsOperationClaims.Read];
}


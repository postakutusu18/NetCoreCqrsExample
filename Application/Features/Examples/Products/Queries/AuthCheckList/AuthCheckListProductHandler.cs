namespace Application.Features.Examples.Products.Queries.AuthCheckList;

public class AuthCheckListProductHandler : IRequestHandler<AuthCheckListProductQuery, IResult>
{
    public Task<IResult> Handle(AuthCheckListProductQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult<IResult>(new SuccessResult());
    }
}


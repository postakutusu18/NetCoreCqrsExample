using Core.Application.Results;
using MediatR;

namespace Application.Features.Products.Queries.GetPermissionCheck;

public class AuthCheckListProductHandler : IRequestHandler<AuthCheckListProductQuery, IResult>
{
    public Task<IResult> Handle(AuthCheckListProductQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult<IResult>(new SuccessResult());
    }
}


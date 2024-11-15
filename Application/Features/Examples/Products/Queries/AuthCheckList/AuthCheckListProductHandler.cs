using Core.Application.Results;
using MediatR;

namespace Application.Features.Example.Products.Queries.AuthCheckList;

public class AuthCheckListProductHandler : IRequestHandler<AuthCheckListProductQuery, IResult>
{
    public Task<IResult> Handle(AuthCheckListProductQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult<IResult>(new SuccessResult());
    }
}


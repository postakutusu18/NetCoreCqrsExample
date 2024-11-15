using Core.Application.Results;
using MediatR;

namespace Application.Features.Users.Auth.Queries.AuthCheckList;

public class AuthCheckListProductHandler : IRequestHandler<AuthCheckListProductQuery, IResult>
{
    public Task<IResult> Handle(AuthCheckListProductQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult<IResult>(new SuccessResult());
    }
}


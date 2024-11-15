using Core.Application.Results;
using MediatR;

namespace Application.Features.Users.Auth.Queries.AuthCheckAdd;

public class AuthCheckAddProductHandler : IRequestHandler<AuthCheckAddProductQuery, IResult>
{
    public Task<IResult> Handle(AuthCheckAddProductQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult<IResult>(new SuccessResult());
    }
}
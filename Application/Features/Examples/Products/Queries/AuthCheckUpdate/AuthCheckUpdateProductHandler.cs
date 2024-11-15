using Core.Application.Results;
using MediatR;

namespace Application.Features.Example.Products.Queries.AuthCheckUpdate;

public class AuthCheckUpdateProductHandler : IRequestHandler<AuthCheckUpdateProductQuery, IResult>
{
    public Task<IResult> Handle(AuthCheckUpdateProductQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult<IResult>(new SuccessResult());
    }
}
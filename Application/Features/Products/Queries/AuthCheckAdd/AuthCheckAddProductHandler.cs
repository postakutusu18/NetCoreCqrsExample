using Core.Application.Results;
using MediatR;

namespace Application.Features.Products.Queries.PermissionCheckAdd;

public class AuthCheckAddProductHandler : IRequestHandler<AuthCheckAddProductQuery, IResult>
{
    public Task<IResult> Handle(AuthCheckAddProductQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult<IResult>(new SuccessResult());
    }
}
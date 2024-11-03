using Application.Features.Products.Constants;
using Core.Application.Pipelines.Authorization;
using Core.Application.Results;
using MediatR;

namespace Application.Features.Products.Queries.UpdatePermissionCheck;

public record AuthCheckUpdateProductQuery() : IRequest<IResult>, ISecuredRequest
{
    public string[] Roles => [ProductsOperationClaims.Update];
}

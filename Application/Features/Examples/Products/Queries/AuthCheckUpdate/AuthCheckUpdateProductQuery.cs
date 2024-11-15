using Application.Features.Example.Products.Constants;
using Core.Application.Pipelines.Authorization;
using Core.Application.Results;
using MediatR;

namespace Application.Features.Example.Products.Queries.AuthCheckUpdate;

public record AuthCheckUpdateProductQuery() : IRequest<IResult>, ISecuredRequest
{
    public string[] Roles => [ProductsOperationClaims.Update];
}

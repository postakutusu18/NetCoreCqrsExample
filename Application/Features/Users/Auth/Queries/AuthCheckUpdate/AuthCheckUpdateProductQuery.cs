using Application.Features.Example.Products.Constants;
using Core.Application.Pipelines.Authorization;
using Core.Application.Results;
using MediatR;

namespace Application.Features.Users.Auth.Queries.AuthCheckUpdate;

public record AuthCheckUpdateProductQuery() : IRequest<IResult>, ISecuredRequest
{
    public string[] Roles => [ProductsOperationClaims.Update];
}

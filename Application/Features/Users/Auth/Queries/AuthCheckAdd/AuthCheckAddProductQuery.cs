using Application.Features.Example.Products.Constants;
using Core.Application.Pipelines.Authorization;
using Core.Application.Results;
using MediatR;

namespace Application.Features.Users.Auth.Queries.AuthCheckAdd;

public record AuthCheckAddProductQuery : IRequest<IResult>, ISecuredRequest
{
    public string[] Roles => [ProductsOperationClaims.Add];
}
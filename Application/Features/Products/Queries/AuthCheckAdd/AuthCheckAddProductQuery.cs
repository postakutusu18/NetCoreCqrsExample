using Application.Features.Products.Constants;
using Core.Application.Pipelines.Authorization;
using Core.Application.Results;
using MediatR;

namespace Application.Features.Products.Queries.PermissionCheckAdd;

public record AuthCheckAddProductQuery : IRequest<IResult>,ISecuredRequest {
    public string[] Roles => [ProductsOperationClaims.Add];
}
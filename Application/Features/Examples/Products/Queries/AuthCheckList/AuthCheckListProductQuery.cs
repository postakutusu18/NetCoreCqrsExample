using Application.Features.Example.Products.Constants;
using Core.Application.Pipelines.Authorization;
using Core.Application.Results;
using MediatR;

namespace Application.Features.Example.Products.Queries.AuthCheckList;

public record AuthCheckListProductQuery() : IRequest<IResult>, ISecuredRequest
{
    public string[] Roles => [ProductsOperationClaims.Read];
}


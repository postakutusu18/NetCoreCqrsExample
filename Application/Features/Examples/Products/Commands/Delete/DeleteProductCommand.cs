using Core.Application.Responses;
using Core.Application.Results;
using MediatR;

namespace Application.Features.Example.Products.Commands.Delete;

public record DeleteProductCommand(Guid Id) : IRequest<IDataResult<DeletedProductResponse>>
{
}

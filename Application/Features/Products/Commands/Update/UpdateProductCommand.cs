using Core.Application.Results;
using MediatR;

namespace Application.Features.Products.Commands.Update;

public record UpdateProductCommand (Guid Id,string Name) : IRequest<IDataResult<UpdatedProductResponse>>
{
}

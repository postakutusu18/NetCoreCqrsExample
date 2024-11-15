namespace Application.Features.Example.Products.Commands.Delete;

public record DeleteProductCommand(Guid Id) : IRequest<IDataResult<DeletedProductResponse>>
{
}

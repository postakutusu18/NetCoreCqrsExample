namespace Application.Features.Example.Products.Commands.Update;

public record UpdateProductCommand(Guid Id, string Name) : IRequest<IDataResult<UpdatedProductResponse>>
{
}

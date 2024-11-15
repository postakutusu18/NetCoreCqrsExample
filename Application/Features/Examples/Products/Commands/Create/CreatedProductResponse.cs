namespace Application.Features.Example.Products.Commands.Create;

public record CreatedProductResponse(Guid Id, string Name) : IResponse { }

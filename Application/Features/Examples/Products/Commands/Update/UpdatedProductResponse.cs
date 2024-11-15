using Core.Application.Responses;

namespace Application.Features.Example.Products.Commands.Update;

public record UpdatedProductResponse(Guid Id, string name) : IResponse { }

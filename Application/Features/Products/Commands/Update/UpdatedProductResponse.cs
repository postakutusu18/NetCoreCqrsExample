using Core.Application.Responses;

namespace Application.Features.Products.Commands.Update;

public record UpdatedProductResponse(Guid Id, string name) : IResponse { }

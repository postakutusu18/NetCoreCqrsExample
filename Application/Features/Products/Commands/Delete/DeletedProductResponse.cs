using Core.Application.Responses;

namespace Application.Features.Products.Commands.Delete;

public record DeletedProductResponse(Guid Id) : IResponse { }
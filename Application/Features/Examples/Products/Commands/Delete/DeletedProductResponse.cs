using Core.Application.Responses;

namespace Application.Features.Example.Products.Commands.Delete;

public record DeletedProductResponse(Guid Id) : IResponse { }
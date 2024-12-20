﻿namespace Application.Features.Example.Products.Commands.Create;

public record CreateProductCommand(string Name) : IRequest<IDataResult<CreatedProductResponse>>, ICacheRemoverRequest, ILoggableRequest
{
    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetAllProducts";
}

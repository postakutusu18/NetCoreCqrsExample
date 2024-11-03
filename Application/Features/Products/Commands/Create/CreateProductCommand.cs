using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Results;
using MediatR;

namespace Application.Features.Products.Commands.Create;

public record CreateProductCommand(string Name) : IRequest<IDataResult<CreatedProductResponse>>, ICacheRemoverRequest, ILoggableRequest
{
    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetAllProducts";
}

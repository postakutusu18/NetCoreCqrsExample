using Core.Application.Requests;
using Core.Application.Responses;
using Core.Application.Results;
using MediatR;

namespace Application.Features.Example.Products.Queries.GetList;

public class GetListProductQuery : IRequest<IDataResult<GetListResponse<GetListProductResponse>>>//, ICachableRequest,ISecuredRequest
{
    public PageRequest PageRequest { get; set; }
    public string[] Roles => new[] { "Write", "Add" };
    public bool BypassCache => false;
    public string CacheKey => $"GetListProducts({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetAllProducts";
    public TimeSpan? SlidingExpiration { get; }
}

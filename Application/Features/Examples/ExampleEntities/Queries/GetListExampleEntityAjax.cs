using Core.Persistance.PagingAjax;
using System.Linq.Expressions;

namespace Application.Features.Examples.ExampleEntities.Queries;

public class GetListExampleEntityAjax : IRequestHandler<GetListAjaxExampleEntityQuery, IDataResult<PagingResult<GetListAjaxExampleEntityResponse>>>
{
    private readonly IUnitOfWorkAsync _unitOfWorkAsync;
    private readonly ExampleEntityRules _exampleEntityRules;
    public GetListExampleEntityAjax(IUnitOfWorkAsync unitOfWorkAsync, ExampleEntityRules exampleEntityRules)
    {
        _unitOfWorkAsync = unitOfWorkAsync;
        _exampleEntityRules = exampleEntityRules;
    }

    public async Task<IDataResult<PagingResult<GetListAjaxExampleEntityResponse>>> Handle(GetListAjaxExampleEntityQuery request, CancellationToken cancellationToken)
    {
        var pageRequest = request.PageRequest;
        Expression<Func<ExampleEntity, bool>>? predicate = null;
        if (pageRequest.IsActive.HasValue)
            predicate = x => x.IsActive == pageRequest.IsActive;
        IPagingResult<ExampleEntity> products = await _unitOfWorkAsync.ExampleEntityRepository.GetListAjaxAsync(
               request.PageRequest, predicate
           );
        var mappedProductListModel = products.Adapt<PagingResult<GetListAjaxExampleEntityResponse>>();
        var resultData = new SuccessDataResult<PagingResult<GetListAjaxExampleEntityResponse>>(mappedProductListModel, "Success List");
        return resultData;
    }


}

public class GetListAjaxExampleEntityQuery : IRequest<IDataResult<PagingResult<GetListAjaxExampleEntityResponse>>>
{
    public DataTableAjaxDto PageRequest { get; set; }
}
public record GetListAjaxExampleEntityResponse(Guid Id, string Name) { }

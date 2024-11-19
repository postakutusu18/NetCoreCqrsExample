using Core.Persistance.PagingAjax;

namespace Application.Features.Examples.ExampleEntities.Queries.GetListAjax;

public class GetListAjaxExampleEntityQuery : IRequest<IDataResult<PagingResult<GetListAjaxExampleEntityResponse>>>
{
    public DataTableAjaxDto PageRequest { get; set; }
}
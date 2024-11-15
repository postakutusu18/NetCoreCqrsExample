namespace Application.Features.Examples.ExampleEntities.Queries.GetList;

public class GetListExampleEntityHandler : IRequestHandler<GetListExampleEntityQuery, IDataResult<GetListResponse<GetListExampleEntityResponse>>>
{
    private readonly IUnitOfWorkAsync _unitOfWorkAsync;
    private readonly ExampleEntityRules _exampleEntityRules;
    public GetListExampleEntityHandler(IUnitOfWorkAsync unitOfWorkAsync, ExampleEntityRules exampleEntityRules)
    {
        _unitOfWorkAsync = unitOfWorkAsync;
        _exampleEntityRules = exampleEntityRules;
    }

    public async Task<IDataResult<GetListResponse<GetListExampleEntityResponse>>> Handle(GetListExampleEntityQuery request, CancellationToken cancellationToken)
    {
        IPaginate<ExampleEntity> products = await _unitOfWorkAsync.ExampleEntityRepository.GetListAsync(
               index: request.PageRequest.PageIndex,
               size: request.PageRequest.PageSize
           );
        var mappedProductListModel = products.Adapt<GetListResponse<GetListExampleEntityResponse>>();
        var resultData = new SuccessDataResult<GetListResponse<GetListExampleEntityResponse>>(mappedProductListModel);
        return resultData;
    }


}
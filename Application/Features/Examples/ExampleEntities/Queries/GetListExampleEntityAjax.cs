﻿using Core.Persistance.PagingAjax;
using System.Linq.Expressions;

namespace Application.Features.Examples.ExampleEntities.Queries;

public class GetListExampleEntityAjax : IRequestHandler<GetListAjaxExampleEntityQuery, IDataResult<PagingResult<GetListAjaxExampleEntityResponse>>>
{
    private readonly IUnitOfWorkAsync _unitOfWorkAsync;
    private readonly ExampleEntityRules _exampleEntityRules;
    private readonly ILocalizationService _localizationService;
    public GetListExampleEntityAjax(IUnitOfWorkAsync unitOfWorkAsync, ExampleEntityRules exampleEntityRules, ILocalizationService localizationService)
    {
        _unitOfWorkAsync = unitOfWorkAsync;
        _exampleEntityRules = exampleEntityRules;
        _localizationService = localizationService;
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
        string message = await _localizationService.GetLocalizedAsync(ExampleEntiesMessages.SuccessList, ExampleEntiesMessages.SectionName);
        var resultData = new SuccessDataResult<PagingResult<GetListAjaxExampleEntityResponse>>(mappedProductListModel,message);
        return resultData;
    }


}

public class GetListAjaxExampleEntityQuery : IRequest<IDataResult<PagingResult<GetListAjaxExampleEntityResponse>>>,
    ISecuredRequest
{
    public DataTableAjaxDto PageRequest { get; set; }

    public string[] Roles => [ExampleEntiesOperationClaims.Admin, ExampleEntiesOperationClaims.Read];
}
public record GetListAjaxExampleEntityResponse(Guid Id, string Name) { }

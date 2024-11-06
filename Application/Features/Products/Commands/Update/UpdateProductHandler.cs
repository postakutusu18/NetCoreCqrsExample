using Application.Features.Products.Rules;
using Application.Repositories;
using Core.Application.Responses;
using Core.Application.Results;
using Domains;
using Domains.Users;
using Mapster;
using MediatR;

namespace Application.Features.Products.Commands.Update;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, IDataResult<UpdatedProductResponse>>
{
    private readonly IUnitOfWorkAsync _unitOfWorkAsync;
    private readonly ProductBusinessRules _productBusinessRules;

    public UpdateProductHandler(IUnitOfWorkAsync unitOfWorkAsync, ProductBusinessRules productBusinessRules)
    {
        _unitOfWorkAsync = unitOfWorkAsync;
        _productBusinessRules = productBusinessRules;
    }

    public async Task<IDataResult<UpdatedProductResponse>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        Product? product = await _unitOfWorkAsync.ProductRepository.GetAsync(predicate: p => p.Id == request.Id, cancellationToken: cancellationToken,enableTracking:false);
        _productBusinessRules.ProductShouldExistWhenSelected(product);
        await _productBusinessRules.ProductNameCanNotBeDuplicatedWhenUpdated(product);

        Product mappedProduct = request.Adapt<Product>();
        await _unitOfWorkAsync.ProductRepository.UpdateAsync(entity: mappedProduct!);
        await _unitOfWorkAsync.SaveAsync();
        var result = mappedProduct.Adapt<UpdatedProductResponse>();
        return new SuccessDataResult<UpdatedProductResponse>(result);
    }
}
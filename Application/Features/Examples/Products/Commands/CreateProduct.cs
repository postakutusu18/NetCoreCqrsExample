namespace Application.Features.Examples.Products.Commands;

public class CreateProduct : IRequestHandler<CreateProductCommand, IDataResult<CreatedProductResponse>>
{
    private readonly IUnitOfWorkAsync _unitOfWorkAsync;
    private readonly ProductBusinessRules _productBusinessRules;
    //private readonly IMailService _mailService;

    public CreateProduct(IUnitOfWorkAsync unitOfWorkAsync, ProductBusinessRules productBusinessRules)//, IMailService mailService
    {
        _unitOfWorkAsync = unitOfWorkAsync;
        _productBusinessRules = productBusinessRules;
        //_mailService = mailService;
    }

    public async Task<IDataResult<CreatedProductResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        await _productBusinessRules.ProductNameCanNotBeDuplicatedWhenInserted(request.Name);
        var product = request.Adapt<Product>();
        Product createdProduct = await _unitOfWorkAsync.ProductRepository.AddAsync(product);
        await _unitOfWorkAsync.SaveAsync();
        CreatedProductResponse createdBrandResponse = createdProduct.Adapt<CreatedProductResponse>();

        var toEmailList = new List<MailboxAddress> { new(name: "Ahmet Çetinkaya", address: "postakutusu1818@gmail.com") };

        //_mailService.SendMail(
        //    new Mail
        //    {
        //        Subject = "New Rental",
        //        TextBody = "A rental has been created.",
        //        ToList = toEmailList
        //    }
        //);
        return new SuccessDataResult<CreatedProductResponse>(createdBrandResponse);
    }
}

public record CreateProductCommand(string Name,decimal Price) : IRequest<IDataResult<CreatedProductResponse>>, ICacheRemoverRequest, ILoggableRequest
{
    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetAllProducts";
}
public record CreatedProductResponse(Guid Id, string Name) : IResponse { }

using Application.Features.Products.Rules;
using Core.Application.Results;
using Domains;
using Mapster;
using MediatR;
using MimeKit;

namespace Application.Features.Products.Commands.Create;

public class CreateProductHandler : IRequestHandler<CreateProductCommand,IDataResult<CreatedProductResponse>>
{
    private readonly IProductRepository _productRepository;
    private readonly ProductBusinessRules _productBusinessRules;
    //private readonly IMailService _mailService;

    public CreateProductHandler(IProductRepository productRepository, ProductBusinessRules productBusinessRules)//, IMailService mailService
    {
        _productRepository = productRepository;
        _productBusinessRules = productBusinessRules;
        //_mailService = mailService;
    }

    public async Task<IDataResult<CreatedProductResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        await _productBusinessRules.ProductNameCanNotBeDuplicatedWhenInserted(request.Name);
        var product = request.Adapt<Product>();
        Product createdProduct = await _productRepository.AddAsync(product);
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

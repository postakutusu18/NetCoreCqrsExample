using Application.Features.Examples.Products.Commands;
using Application.Features.Examples.Products.Queries;
using Core.Application.Requests;
using Core.Application.Results;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Examples;

[ApiController]
[Route("api/[controller]")]
public class ProductController : BaseController
{

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateProductCommand productDto)
    {
        var productId = await Mediator.Send(productDto);
        return Ok(productId);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts([FromQuery] PageRequest pageRequest)
    {
        var query = new GetListProductQuery { PageRequest = pageRequest };
        var products = await Mediator.Send(query);
        return Ok(products);
    }

    [HttpGet("{Id}")]

    public async Task<IActionResult> GetByIdProduct([FromRoute] GetByIdProductQuery query)
    {
        var products = await Mediator.Send(query);
        return Ok(products);
    }
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateProductCommand updateProductCommand)
    {
        IDataResult<UpdatedProductResponse> result = await Mediator.Send(updateProductCommand);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteProductCommand deleteProductCommand)
    {
        IDataResult<DeletedProductResponse> result = await Mediator.Send(deleteProductCommand);
        return Ok(result);
    }

    [HttpGet("CheckListAuthProduct")]
    public async Task<IActionResult> AuthCheckListProduct([FromRoute] AuthCheckListProductQuery listCheck)
    {
        var result = await Mediator.Send(listCheck);
        return Ok(result);
    }

    [HttpGet("CheckAddAuthProduct")]
    public async Task<IActionResult> AuthCheckAddProduct([FromRoute] AuthCheckAddProductQuery addCheck)
    {
        var result = await Mediator.Send(addCheck);
        return Ok(result);
    }

    [HttpGet("CheckUpdateAuthProduct")]
    public async Task<IActionResult> AuthCheckUpdateProduct([FromRoute] AuthCheckUpdateProductQuery updateCheck)
    {
        var result = await Mediator.Send(updateCheck);
        return Ok(result);
    }
}

﻿using Application.Features.Products.Commands.Create;
using Application.Features.Products.Commands.Delete;
using Application.Features.Products.Commands.Update;
using Application.Features.Products.Queries.GetById;
using Application.Features.Products.Queries.GetList;
using Application.Features.Products.Queries.GetPermissionCheck;
using Application.Features.Products.Queries.PermissionCheckAdd;
using Application.Features.Products.Queries.UpdatePermissionCheck;
using Core.Application.Requests;
using Core.Application.Results;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

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

    [HttpGet("AuthCheckListProduct")]
    public async Task<IActionResult> AuthCheckListProduct(AuthCheckListProductQuery listCheck)
    {
        var result = await Mediator.Send(listCheck);
        return Ok(result);
    }

    [HttpGet("AuthCheckAddProduct")]
    public async Task<IActionResult> AuthCheckAddProduct(AuthCheckAddProductQuery addCheck)
    {
        var result = await Mediator.Send(addCheck);
        return Ok(result);
    }

    [HttpGet("AuthCheckUpdateProduct")]
    public async Task<IActionResult> AuthCheckUpdateProduct(AuthCheckUpdateProductQuery updateCheck)
    {
        var result = await Mediator.Send(updateCheck);
        return Ok(result);
    }
}

using Application.Features.Examples.ExampleEntities.Commands.Create;
using Application.Features.Examples.ExampleEntities.Commands.CreateRange;
using Application.Features.Examples.ExampleEntities.Commands.Delete;
using Application.Features.Examples.ExampleEntities.Commands.Update;
using Application.Features.Examples.ExampleEntities.Queries.CheckAddAuth;
using Application.Features.Examples.ExampleEntities.Queries.CheckListAuth;
using Application.Features.Examples.ExampleEntities.Queries.CheckUpdateAuth;
using Application.Features.Examples.ExampleEntities.Queries.GetById;
using Application.Features.Examples.ExampleEntities.Queries.GetList;
using Application.Features.Examples.ExampleEntities.Queries.GetListAjax;
using Core.Application.Requests;
using Core.Application.Results;
using Core.Persistance.PagingAjax;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebApi.Controllers.Examples;

[ApiController]
[Route("api/[controller]")]
public class ExampleEntityController : BaseController
{

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateExampleEntityCommand productDto)
    {
        var productId = await Mediator.Send(productDto);
        return Ok(productId);
    }
    [HttpPost("AddRange")]
    public async Task<IActionResult> AddRange([FromBody] CreateRangeExampleEntityCommand productsDto)
    {
        var productId = await Mediator.Send(productsDto);
        return Ok(productId);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllExampleEntities([FromQuery] PageRequest pageRequest)
    {
        var query = new GetListExampleEntityQuery { PageRequest = pageRequest };
        var products = await Mediator.Send(query);
        return Ok(products);
    }

    [HttpGet("GetAllAjaxExampleEntities")]
    public async Task<IActionResult> GetAllAjaxExampleEntities([FromQuery] DataTableAjaxDto pageRequest)
    {
        var query = new GetListAjaxExampleEntityQuery { PageRequest = pageRequest };
        var products = await Mediator.Send(query);
        return Ok(products);
    }
    [HttpGet("{Id}")]

    public async Task<IActionResult> GetByIdExampleEntity([FromRoute] GetByIdExampleEntityQuery query)
    {
        var products = await Mediator.Send(query);
        return Ok(products);
    }
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateExampleEntityCommand updateExampleEntityCommand)
    {
        IDataResult<UpdatedExampleEntityResponse> result = await Mediator.Send(updateExampleEntityCommand);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteExampleEntityCommand deleteExampleEntityCommand)
    {
        IDataResult<DeletedExampleEntityResponse> result = await Mediator.Send(deleteExampleEntityCommand);
        return Ok(result);
    }

    [HttpGet("AuthCheckListExampleEntity")]
    public async Task<IActionResult> AuthCheckListExampleEntity([FromRoute] CheckListAuthExampleEntityQuery listCheck)
    {
        var result = await Mediator.Send(listCheck);
        return Ok(result);
    }

    [HttpGet("AuthCheckAddExampleEntity")]
    public async Task<IActionResult> AuthCheckAddExampleEntity([FromRoute] CheckAddAuthExampleEntityQuery addCheck)
    {
        var result = await Mediator.Send(addCheck);
        return Ok(result);
    }

    [HttpGet("AuthCheckUpdateExampleEntity")]
    public async Task<IActionResult> AuthCheckUpdateExampleEntity([FromRoute] AuthCheckUpdateExampleEntityQuery updateCheck)
    {
        var result = await Mediator.Send(updateCheck);
        return Ok(result);
    }
   
}

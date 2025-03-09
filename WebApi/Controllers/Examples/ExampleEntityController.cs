using Application.Features.Examples.ExampleEntities.Commands;
using Application.Features.Examples.ExampleEntities.Queries;
using Core.Application.Requests;
using Core.Application.Results;
using Core.Persistance.PagingAjax;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet("CheckListAuthExampleEntity")]
    public async Task<IActionResult> CheckListAuthExampleEntity([FromRoute] CheckListAuthExampleEntityQuery listCheck)
    {
        var result = await Mediator.Send(listCheck);
        return Ok(result);
    }

    [HttpGet("CheckAddAuthExampleEntity")]
    public async Task<IActionResult> CheckAddAuthExampleEntity([FromRoute] CheckAddAuthExampleEntityQuery addCheck)
    {
        var result = await Mediator.Send(addCheck);
        return Ok(result);
    }

    [HttpGet("CheckUpdateAuthExampleEntity")]
    public async Task<IActionResult> CheckUpdateAuthExampleEntity([FromRoute] CheckUpdateAuthExampleEntityQuery updateCheck)
    {
        var result = await Mediator.Send(updateCheck);
        return Ok(result);
    }
    [HttpGet("AnyExampleEntity/{Id}")]
    public async Task<IActionResult> AnyExampleEntity([FromRoute] AnyExampleEntityQuery anyExampleEntity)
    {
        var result = await Mediator.Send(anyExampleEntity);
        return Ok(result);
    }

    [HttpGet("GetCountExampleEntity")]
    public async Task<IActionResult> GetCountExampleEntity()
    {
        var result = await Mediator.Send(new GetCountExampleEntityQuery());
        return Ok(result);
    }

    [HttpPost("DeleteSqlExampleEntity")]
    public async Task<IActionResult> DeleteSqlExampleEntity([FromBody] DeleteSqlExampleEntityCommand deleteSqlExample)
    {
        var result = await Mediator.Send(deleteSqlExample);
        return Ok(result);
    }
}

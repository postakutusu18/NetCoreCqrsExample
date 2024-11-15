using Application.Features.Examples.ExampleEntities.Commands.Create;
using Application.Features.Examples.ExampleEntities.Commands.Delete;
using Application.Features.Examples.ExampleEntities.Commands.Update;
using Application.Features.Examples.ExampleEntities.Queries.AuthCheckAdd;
using Application.Features.Examples.ExampleEntities.Queries.AuthCheckList;
using Application.Features.Examples.ExampleEntities.Queries.AuthCheckUpdate;
using Application.Features.Examples.ExampleEntities.Queries.GetById;
using Application.Features.Examples.ExampleEntities.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Results;
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

    [HttpGet]
    public async Task<IActionResult> GetAllExampleEntities([FromQuery] PageRequest pageRequest)
    {
        var query = new GetListExampleEntityQuery { PageRequest = pageRequest };
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
    public async Task<IActionResult> AuthCheckListExampleEntity(AuthCheckListExampleEntityHandler listCheck)
    {
        var result = await Mediator.Send(listCheck);
        return Ok(result);
    }

    [HttpGet("AuthCheckAddExampleEntity")]
    public async Task<IActionResult> AuthCheckAddExampleEntity(AuthCheckAddExampleEntityQuery addCheck)
    {
        var result = await Mediator.Send(addCheck);
        return Ok(result);
    }

    [HttpGet("AuthCheckUpdateExampleEntity")]
    public async Task<IActionResult> AuthCheckUpdateExampleEntity(AuthCheckUpdateExampleEntityQuery updateCheck)
    {
        var result = await Mediator.Send(updateCheck);
        return Ok(result);
    }
}

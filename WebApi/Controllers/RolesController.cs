using Application.Features.Roles.Commands.Create;
using Application.Features.Roles.Commands.Delete;
using Application.Features.Roles.Commands.Update;
using Application.Features.Roles.Queries.GetById;
using Application.Features.Roles.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RolesController : BaseController
{
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdRoleQuery getByIdRoleQuery)
    {
       var result = await Mediator.Send(getByIdRoleQuery);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListRoleQuery getListRoleQuery = new() { PageRequest = pageRequest };
        var result = await Mediator.Send(getListRoleQuery);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateRoleCommand createRoleCommand)
    {
        var result = await Mediator.Send(createRoleCommand);
        return Created(uri: "", result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateRoleCommand updateRoleCommand)
    {
        var result = await Mediator.Send(updateRoleCommand);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteRoleCommand deleteRoleCommand)
    {
        var result = await Mediator.Send(deleteRoleCommand);
        return Ok(result);
    }
}
using Application.Features.Examples.ExampleEntities.Queries;
using Application.Features.Users.Roles.Commands;
using Application.Features.Users.Roles.Queries;
using Core.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Users;

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

    [HttpGet("CheckAddAuthListRole")]
    public async Task<IActionResult> CheckAddAuthListRole([FromRoute] CheckListAuthRoleQuery listCheck)
    {
        var result = await Mediator.Send(listCheck);
        return Ok(result);
    }

    [HttpGet("CheckAddAuthAddRole")]
    public async Task<IActionResult> CheckAddAuthAddRole([FromRoute] CheckAddAuthRoleQuery addCheck)
    {
        var result = await Mediator.Send(addCheck);
        return Ok(result);
    }

    [HttpGet("CheckAddAuthUpdateRole")]
    public async Task<IActionResult> CheckAddAuthUpdateRole([FromRoute] CheckUpdateAuthRoleQuery updateCheck)
    {
        var result = await Mediator.Send(updateCheck);
        return Ok(result);
    }
}
using Application.Features.UserRoles.Commands.Create;
using Application.Features.UserRoles.Commands.Delete;
using Application.Features.UserRoles.Commands.Update;
using Application.Features.UserRoles.Queries.GetById;
using Application.Features.UserRoles.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserRolesController : BaseController
{
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdUserRoleQuery getByIdUserRoleQuery)
    {
        var result = await Mediator.Send(getByIdUserRoleQuery);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListUserRoleQuery getListUserRoleQuery = new() { PageRequest = pageRequest };
        var result = await Mediator.Send(getListUserRoleQuery);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateUserRoleCommand createUserRoleCommand)
    {
        var result = await Mediator.Send(createUserRoleCommand);
        return Created(uri: "", result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateUserRoleCommand updateUserRoleCommand)
    {
        var result = await Mediator.Send(updateUserRoleCommand);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteUserRoleCommand deleteUserRoleCommand)
    {
        var result = await Mediator.Send(deleteUserRoleCommand);
        return Ok(result);
    }
}
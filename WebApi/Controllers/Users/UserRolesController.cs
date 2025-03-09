using Application.Features.UserRoles.Commands.Update;
using Application.Features.UserRoles.Queries.GetById;
using Application.Features.UserRoles.Queries.GetList;
using Application.Features.Users.UserRoles.Commands;
using Application.Features.Users.UserRoles.Queries;
using Core.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Users;

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

    [HttpGet("CheckAddAuthListUserRole")]
    public async Task<IActionResult> CheckAddAuthListUserRole([FromRoute] CheckListAuthUserRoleQuery listCheck)
    {
        var result = await Mediator.Send(listCheck);
        return Ok(result);
    }

    [HttpGet("CheckAddAuthAddUserRole")]
    public async Task<IActionResult> CheckAddAuthAddUserRole([FromRoute] CheckAddAuthUserRoleQuery addCheck)
    {
        var result = await Mediator.Send(addCheck);
        return Ok(result);
    }

    [HttpGet("CheckAddAuthUpdateUserRole")]
    public async Task<IActionResult> CheckAddAuthUpdateUserRole([FromRoute] CheckUpdateAuthUserRoleQuery updateCheck)
    {
        var result = await Mediator.Send(updateCheck);
        return Ok(result);
    }
}
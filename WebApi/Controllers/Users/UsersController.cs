using Application.Features.UserFeatures.Users.Commands.Create;
using Application.Features.UserFeatures.Users.Commands.Delete;
using Application.Features.UserFeatures.Users.Commands.Update;
using Application.Features.Users.Commands.UpdateFromAuth;
using Application.Features.Users.Queries.GetById;
using Application.Features.Users.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Users;

[Route("api/[controller]")]
[ApiController]
public class UsersController : BaseController
{
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdUserQuery getByIdUserQuery)
    {
        var result = await Mediator.Send(getByIdUserQuery);
        return Ok(result);
    }

    [HttpGet("GetFromAuth")]
    public async Task<IActionResult> GetFromAuth()
    {
        GetByIdUserQuery getByIdUserQuery = new() { Id = getUserIdFromRequest() };
        var result = await Mediator.Send(getByIdUserQuery);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListUserQuery getListUserQuery = new() { PageRequest = pageRequest };
        var result = await Mediator.Send(getListUserQuery);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateUserCommand createUserCommand)
    {
        var result = await Mediator.Send(createUserCommand);
        return Created(uri: "", result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateUserCommand updateUserCommand)
    {
        var result = await Mediator.Send(updateUserCommand);
        return Ok(result);
    }

    [HttpPut("FromAuth")]
    public async Task<IActionResult> UpdateFromAuth([FromBody] UpdateUserFromAuthCommand updateUserFromAuthCommand)
    {
        updateUserFromAuthCommand.Id = getUserIdFromRequest();
        var result = await Mediator.Send(updateUserFromAuthCommand);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteUserCommand deleteUserCommand)
    {
        var result = await Mediator.Send(deleteUserCommand);
        return Ok(result);
    }
}

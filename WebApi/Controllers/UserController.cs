﻿using Application.Features.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Authorize(Roles = "Admin")]
[ApiController, Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var request = new GetAllUsersQuery();

        var response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpGet("{username}")]
    public async Task<IActionResult> GetByUsername(
        string username)
    {
        var request = new GetUserByUsernameQuery
        {
            Username = username
        };

        var response = await _mediator.Send(request);

        return Ok(response ?? new object());
    }

    [HttpPost]
    public async Task<IActionResult> Add(
        AddUserCommand request)
    {
        var response = await _mediator.Send(request);

        return Created("users/" + response.Username, response);
    }

    [HttpPatch("{username}/profile")]
    public async Task<IActionResult> PatchProfile(
        PatchUserProfileProps props, string username)
    {
        var request = new PatchUserProfileCommand
        {
            Username = username,
            Props = props,
        };

        await _mediator.Send(request);

        return Ok();
    }

    [HttpPatch("role")]
    public async Task<IActionResult> SetRole(
        SetUserRoleCommand request)
    {
        await _mediator.Send(request);

        return Ok();
    }

    [HttpDelete("{username}")]
    public async Task<IActionResult> Delete(
        string username)
    {
        var request = new DeleteUserCommand
        {
            Username = username,
        };

        await _mediator.Send(request);

        return Ok();
    }
}

﻿using Application.Features.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

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
    public async Task<IActionResult> GetByUsername(string username)
    {
        var request = new GetByUsernameQuery
        {
            Username = username
        };

        var response = await _mediator.Send(request);

        return Ok(response ?? new object());
    }

    [HttpDelete]
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

    [HttpPost]
    public async Task<IActionResult> Add(
        AddUserCommand request)
    {
        var response = await _mediator.Send(request);

        return Created("users/" + response.Username, response);
    }
}

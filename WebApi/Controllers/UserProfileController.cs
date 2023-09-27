using Application.Features.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApi.Controllers;

[ApiController, Route("api/profiles")]
public class UserProfileController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserProfileController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize(Roles = "Admin")]
    [HttpPatch("{username}")]
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

    [HttpPatch]
    public async Task<IActionResult> PatchOwnProfile(
        PatchUserProfileProps props)
    {
        var username = HttpContext.User.FindFirstValue(ClaimTypes.Name);

        var request = new PatchUserProfileCommand
        {
            Username = username ?? string.Empty,
            Props = props,
        };

        await _mediator.Send(request);

        return Ok();
    }
}

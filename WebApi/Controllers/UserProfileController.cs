using Application.Features.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApi.Controllers;

[ApiController, Route("api/profile")]
public class UserProfileController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserProfileController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetOwnProfile()
    {
        var username = HttpContext.User.FindFirstValue(ClaimTypes.Name);

        var request = new GetUserByUsernameQuery
        {
            Username = username ?? string.Empty,
        };

        var response = await _mediator.Send(request);

        return Ok(response?.Profile ?? new object());
    }

    //[Authorize(Roles = "Admin")]
    [AllowAnonymous]
    [HttpPatch("{username}")]
    public async Task<IActionResult> PatchProfile(
        PatchUserProfileProps props, string username)
    {
        var request = new PatchUserProfileCommand
        {
            Username = username,
            Props = props,
        };

        var result = await _mediator.Send(request);

        if (result.IsSuccess)
            return Ok();
        
        return BadRequest(result.Reasons);
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

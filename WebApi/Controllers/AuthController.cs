using Application.Features.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebApi.Helpers;

namespace WebApi.Controllers;

[ApiController, Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IConfiguration _configuration;

    public AuthController(IMediator mediator, IConfiguration configuration)
    {
        _mediator = mediator;
        _configuration = configuration;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        string username,
        string password)
    {
        const string ErrorMessage = "Either username or password are incorrect.";

        var request = new VerifyPasswordQuery
        {
            Username = username,
            Password = password,
        };

        var passwordVerified = await _mediator.Send(request);
        if (!passwordVerified)
            return Unauthorized(ErrorMessage);

        var userRequest = new GetUserByUsernameQuery
        {
            Username = username,
        };

        var user = await _mediator.Send(userRequest);
        if (user == null)
            return Unauthorized(ErrorMessage);

        var expiration = DateTime.UtcNow.AddMinutes(30);
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim(ClaimTypes.GivenName, user.Profile.FirstName + " " + user.Profile.LastName),
            new Claim(ClaimTypes.Expiration, expiration.ToString()),
        };

        var token = AuthHelper.GetTokenString(
            claims, expiration, _configuration);

        return Ok(
            new
            {
                Token = token,
                Expiration = expiration,
            });
    }
}

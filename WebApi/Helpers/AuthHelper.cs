using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApi.Helpers;

public static class AuthHelper
{
    public static string GetTokenString(
        List<Claim> claims, DateTime expiration, IConfiguration configuration)
    {
        var securityKey = configuration.GetJwtSecurityKey();

        var token = new JwtSecurityToken(
            claims: claims,
            expires: expiration,
            signingCredentials: new SigningCredentials(
                securityKey, SecurityAlgorithms.HmacSha256));

        var handler = new JwtSecurityTokenHandler();

        return handler.WriteToken(token);
    }

    public static SymmetricSecurityKey GetJwtSecurityKey(
        this IConfiguration configuration)
    {
        var key = configuration["JwtKey"] ??
            throw new Exception("JWT key was not specified.");

        var securityKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(key));

        return securityKey;
    }
}

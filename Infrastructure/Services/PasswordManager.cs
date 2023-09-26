using Application.Shared;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

internal class PasswordManager : IPasswordManager
{
    private readonly AppDbContext _context;

    public PasswordManager(AppDbContext context)
    {
        _context = context;
    }

    public string Hash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, 15);
    }

    public async Task<bool> VerifyAsync(string username, string password)
    {
        var hash = await _context.Users
            .Where(u => u.Username == username)
            .Select(u => u.PasswordHash)
            .FirstOrDefaultAsync() ?? string.Empty;

        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}

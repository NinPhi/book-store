using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<List<User>> GetAllAsync()
    {
        return _context.Users.ToListAsync();
    }

    public Task<User?> GetByIdAsync(long id)
    {
        return _context.Users.FindAsync(id).AsTask();
    }

    public Task<User?> GetByUsernameAsync(string username)
    {
        return _context.Users.FirstOrDefaultAsync(u => u.Username == username);
    }

    public Task<bool> ExistsAsync(string username)
    {
        return _context.Users.AnyAsync(u => u.Username == username);
    }

    public void Add(User entity)
    {
        _context.Users.Add(entity);
    }

    public void Update(User entity)
    {
        _context.Users.Update(entity);

    }

    public void Delete(User entity)
    {
        _context.Users.Remove(entity);
    }
}

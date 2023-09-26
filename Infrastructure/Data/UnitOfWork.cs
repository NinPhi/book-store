using Domain.Shared;

namespace Infrastructure.Data;

internal class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public Task CommitAsync()
    {
        return _context.SaveChangesAsync();
    }
}

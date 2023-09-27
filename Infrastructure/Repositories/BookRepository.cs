using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal class BookRepository : IBookRepository
{
    private readonly AppDbContext _context;

    public BookRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<List<Book>> GetAllAsync()
    {
        return _context.Books.AsNoTracking().ToListAsync();
    }

    public async Task<Book?> GetByIdAsync(long id)
    {
        var entity = await _context.Books.FindAsync(id);

        if (entity != null)
            _context.Entry(entity).State = EntityState.Detached;

        return entity;
    }

    public Task<Book?> GetByIsbnAsync(string Isbn)
    {
        return _context.Books.AsNoTracking()
            .FirstOrDefaultAsync(b => b.Isbn == Isbn);
    }

    public void Add(Book entity)
    {
        _context.Books.Add(entity);
    }

    public void Update(Book entity)
    {
        _context.Books.Update(entity);
    }

    public void Delete(Book entity)
    {
        _context.Books.Remove(entity);
    }
}

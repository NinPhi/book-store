using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data;

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
        throw new NotImplementedException();
    }

    public Task<Book?> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<Book?> GetByTitle(string title)
    {
        throw new NotImplementedException();
    }

    public void Add(Book entity)
    {
        throw new NotImplementedException();
    }

    public void Update(Book entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(Book entity)
    {
        throw new NotImplementedException();
    }
}

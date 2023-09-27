using Application.Services;
using Domain.Entities;

namespace Infrastructure.Services;

internal class TodoApi : ITodoApi
{
    public Task<List<Book>> GetBooks(int count)
    {
        throw new NotImplementedException();
    }
}

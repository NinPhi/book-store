namespace Application.Services;

public interface ITodoApi
{
    Task<List<Book>> GetBooks(int count);
}

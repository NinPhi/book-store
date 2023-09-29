namespace Domain.Exceptions;

public class BookNotFoundException : Exception
{
    public BookNotFoundException(string isbn)
        : base($"Book with ISBN {isbn} was not found") { }
}

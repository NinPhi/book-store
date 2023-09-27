namespace Application.Features.Books;

public record SearchBooksQuery : IRequest<List<BookDto>>
{
    public string? Title { get; init; }
    public string? Author { get; init; }
}

internal class SearchBooksQueryHandler
    : IRequestHandler<SearchBooksQuery, List<BookDto>>
{
    private readonly IBookRepository _bookRepository;

    public SearchBooksQueryHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<List<BookDto>> Handle(
        SearchBooksQuery request, CancellationToken cancellationToken)
    {
        var books = await _bookRepository.SearchAsync(
            request.Title, request.Author);

        var response = books.Adapt<List<BookDto>>();

        return response;
    }
}

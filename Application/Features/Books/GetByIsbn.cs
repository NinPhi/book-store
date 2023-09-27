namespace Application.Features.Books;

public record GetBookByIsbnQuery : IRequest<BookDto?>
{
    public required string Isbn { get; init; }
}

internal class GetBookByIsbnQueryHandler
    : IRequestHandler<GetBookByIsbnQuery, BookDto?>
{
    private readonly IBookRepository _bookRepository;

    public GetBookByIsbnQueryHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<BookDto?> Handle(
        GetBookByIsbnQuery request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIsbnAsync(request.Isbn);

        var response = book.Adapt<BookDto>();

        return response;
    }
}

namespace Application.Features.Books;

public record AddBookCommand : IRequest<BookDto>
{
    public required string Isbn { get; init; }
    public required string Title { get; init; }
    public required string Author { get; init; }
    public required int Count { get; set; }
}

internal class AddBookCommandHandler
    : IRequestHandler<AddBookCommand, BookDto>
{
    private readonly IBookRepository _bookRepository;
    private readonly IUnitOfWork _uow;

    public AddBookCommandHandler(IBookRepository bookRepository, IUnitOfWork uow)
    {
        _bookRepository = bookRepository;
        _uow = uow;
    }

    public async Task<BookDto> Handle(
        AddBookCommand request, CancellationToken cancellationToken)
    {
        var book = request.Adapt<Book>();

        _bookRepository.Add(book);
        await _uow.CommitAsync();

        var response = book.Adapt<BookDto>();

        return response;
    }
}

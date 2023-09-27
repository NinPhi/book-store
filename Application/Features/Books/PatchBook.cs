namespace Application.Features.Books;

public record PatchBookCommand : IRequest
{
    public required string Isbn { get; init; }

    public required PatchBookProps Props { get; init; }
}

public record PatchBookProps
{
    public string? Title { get; init; }
    public string? Author { get; init; }
    public int? Count { get; init; }
}

internal class PatchBookCommandHandler
    : IRequestHandler<PatchBookCommand>
{
    private readonly IBookRepository _bookRepository;
    private readonly IUnitOfWork _uow;

    public PatchBookCommandHandler(IBookRepository bookRepository, IUnitOfWork uow)
    {
        _bookRepository = bookRepository;
        _uow = uow;
    }

    public async Task Handle(
        PatchBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIsbnAsync(request.Isbn)
            ?? throw new Exception("Book with the specified Isbn was not found.");

        if (!string.IsNullOrWhiteSpace(request.Props.Title))
            book.Title = request.Props.Title;

        if (!string.IsNullOrWhiteSpace(request.Props.Author))
            book.Author = request.Props.Author;

        if (request.Props.Count.HasValue)
            book.Count = request.Props.Count.Value;

        _bookRepository.Update(book);
        await _uow.CommitAsync();
    }
}

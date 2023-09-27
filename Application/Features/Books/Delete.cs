namespace Application.Features.Books;

public record DeleteBookCommand : IRequest
{
    public required string Isbn { get; init; }
}

internal class DeleteBookCommandHandler
    : IRequestHandler<DeleteBookCommand>
{
    private readonly IBookRepository _bookRepository;
    private readonly IUnitOfWork _uow;

    public DeleteBookCommandHandler(IBookRepository bookRepository, IUnitOfWork uow)
    {
        _bookRepository = bookRepository;
        _uow = uow;
    }

    public async Task Handle(
        DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIsbnAsync(request.Isbn);

        if (book == null) return;

        _bookRepository.Delete(book);
        await _uow.CommitAsync();
    }
}

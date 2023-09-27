namespace Application.Features.Books;

public record SeedDummyBookDataCommand : IRequest { }

internal class SeedDummyBookDataCommandHandler
    : IRequestHandler<SeedDummyBookDataCommand>
{
    private readonly IBookRepository _bookRepository;
    private readonly IUnitOfWork _uow;

    public SeedDummyBookDataCommandHandler(IBookRepository bookRepository, IUnitOfWork uow)
    {
        _bookRepository = bookRepository;
        _uow = uow;
    }

    public Task Handle(
        SeedDummyBookDataCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

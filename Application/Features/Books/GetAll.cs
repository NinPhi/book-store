namespace Application.Features.Books;

public record GetAllBooksQuery : IRequest<List<BookDto>> { }

public class GetAllBooksQueryHandler
    : IRequestHandler<GetAllBooksQuery, List<BookDto>>
{
    private readonly IBookRepository _bookRepository;

    public GetAllBooksQueryHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<List<BookDto>> Handle(
        GetAllBooksQuery request, CancellationToken cancellationToken)
    {
        var books = await _bookRepository.GetAllAsync();

        var response = books.Adapt<List<BookDto>>();

        return response;
    }
}

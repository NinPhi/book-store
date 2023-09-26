using Application.Contracts;
using Domain.Repositories;
using Mapster;

namespace Application.Features.Books;

public record GetBookByTitleAndAuthorQuery : IRequest<BookDto?>
{
    public required string Title { get; init; }
    public required string Author { get; init; }
}

internal class GetBookByTitleAndAuthorQueryHandler
    : IRequestHandler<GetBookByTitleAndAuthorQuery, BookDto?>
{
    private readonly IBookRepository _bookRepository;

    public GetBookByTitleAndAuthorQueryHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<BookDto?> Handle(
        GetBookByTitleAndAuthorQuery request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository
            .GetByTitleAndAuthorAsync(
                request.Title, request.Author);

        var response = book.Adapt<BookDto>();

        return response;
    }
}

using Application.Contracts;
using Domain.Repositories;
using Mapster;

namespace Application.Features.Books;

public record GetBookByTitleQuery : IRequest<BookDto?>
{
    public required string Title { get; init; }
}

internal class GetBookByTitleQueryHandler
    : IRequestHandler<GetBookByTitleQuery, BookDto?>
{
    private readonly IBookRepository _bookRepository;

    public GetBookByTitleQueryHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<BookDto?> Handle(
        GetBookByTitleQuery request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByTitleAsync(request.Title);

        var response = book.Adapt<BookDto>();

        return response;
    }
}

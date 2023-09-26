using Application.Features.Books;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController, Route("api/books")]
public class BookController : ControllerBase
{
    private readonly IMediator _mediator;

    public BookController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var request = new GetAllBooksQuery();

        var response = await _mediator.Send(request);

        return Ok(response);
    }

    [AllowAnonymous]
    [HttpGet("{title}")]
    public async Task<IActionResult> GetByTitle(
        string title, string author)
    {
        var request = new GetBookByTitleAndAuthorQuery
        {
            Title = title,
            Author = author,
        };

        var response = await _mediator.Send(request);

        return Ok(response);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Add(
        AddBookCommand request)
    {
        var response = await _mediator.Send(request);

        return Created("api/books/" + Uri.EscapeDataString(response.Title), response ?? new object());
    }
}

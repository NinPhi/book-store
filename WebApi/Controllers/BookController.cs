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
    [HttpGet("{Isbn}")]
    public async Task<IActionResult> GetByTitle(
        string Isbn)
    {
        var request = new GetBookByIsbnQuery
        {
            Isbn = Isbn,
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

    [Authorize(Roles = "Admin")]
    [HttpPut("{Isbn}")]
    public async Task<IActionResult> Edit(
        PatchBookProps props, string Isbn)
    {
        var request = new PatchBookCommand
        {
            Isbn = Isbn,
            Props = props,
        };

        await _mediator.Send(request);

        return Ok();
    }
}

using Application.Features.Books;
using Domain.Exceptions;
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
    [HttpGet("{isbn}")]
    public async Task<IActionResult> GetByTitle(
        string isbn)
    {
        var request = new GetBookByIsbnQuery
        {
            Isbn = isbn,
        };

        var response = await _mediator.Send(request);

        return Ok(response);
    }

    [AllowAnonymous]
    [HttpGet("search")]
    public async Task<IActionResult> Search(
        string? title = null, string? author = null)
    {
        var request = new SearchBooksQuery
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

    [Authorize(Roles = "Admin")]
    [HttpPatch("{isbn}")]
    public async Task<IActionResult> Patch(
        PatchBookProps props, string isbn)
    {
        try
        {
            var request = new PatchBookCommand
            {
                Isbn = isbn,
                Props = props,
            };

            await _mediator.Send(request);

            return Ok();
        }
        catch (BookNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{isbn}")]
    public async Task<IActionResult> Delete(
        string isbn)
    {
        var request = new DeleteBookCommand
        {
            Isbn = isbn,
        };

        await _mediator.Send(request);

        return Ok();
    }
}

namespace Application.Contracts;

public record BookDto
{
    public required long Id { get; init; }
    public required string ISBN { get; init; }
    public required string Title { get; init; }
    public required string Author { get; init; }
    public required int Count { get; set; }
}

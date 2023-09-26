using Domain.Enums;

namespace Application.Contracts;

public record UserDto
{
    public required long Id { get; init; }
    public required string Username { get; init; }
    public required UserRole Role { get; init; }
}

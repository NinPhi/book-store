namespace Application.Contracts;

public record UserProfileDto
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required DateTime Dob { get; init; }
    public required string Address { get; init; }
}

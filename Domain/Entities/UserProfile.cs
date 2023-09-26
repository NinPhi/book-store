using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class UserProfile
{
    public long UserId { get; set; }

    [StringLength(200)]
    public required string FirstName { get; set; }

    [StringLength(200)]
    public required string LastName { get; set; }

    [StringLength(300)]
    public required string Email { get; set; }

    public required DateTime Dob { get; set; }

    public User? User { get; set; }
}

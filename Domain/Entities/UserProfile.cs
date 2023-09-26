using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class UserProfile
{
    public long UserId { get; set; }

    [StringLength(200)]
    public required string FirstName { get; set; }

    [StringLength(200)]
    public required string LastName { get; set; }

    public required DateTime Dob { get; set; }

    [StringLength(600)]
    public required string Address { get; set; }

    public User? User { get; set; }
}

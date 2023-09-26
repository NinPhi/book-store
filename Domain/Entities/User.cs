using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class User
{
    public long Id { get; set; }

    [StringLength(100)]
    public required string Username { get; set; }

    [StringLength(500)]
    public required string PasswordHash { get; set; }

    public required UserRole Role { get; set; }

    public required UserProfile Profile { get; set; }
}

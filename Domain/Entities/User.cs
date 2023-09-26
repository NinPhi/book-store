using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class User
{
    public long Id { get; set; }
    public required string Username { get; set; }
    public required string PasswordHash { get; set; }
    public required UserRole Role { get; set; }
}

using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

internal class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var user = modelBuilder.Entity<User>();
        user.HasIndex(c => c.Username).IsUnique();
        user.Property(c => c.Username)
            // Makes username case-insensitive
            .UseCollation("SQL_Latin1_General_CP1_CI_AS");
        user.Navigation(u => u.Profile).AutoInclude();

        var userProfile = modelBuilder.Entity<UserProfile>();
        userProfile.ToTable("UserProfiles");
        userProfile.HasKey(up => up.UserId);
    }

    public DbSet<User> Users => Set<User>();
}

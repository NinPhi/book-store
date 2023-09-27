using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Book
{
    public long Id { get; set; }

    [StringLength(100)]
    public required string Isbn { get; set; }

    [StringLength(400)]
    public required string Title { get; set; }

    [StringLength(400)]
    public required string Author { get; set; }

    public int Count { get; set; }
}

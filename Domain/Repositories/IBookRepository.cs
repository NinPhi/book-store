﻿using Domain.Entities;
using Domain.Shared;

namespace Domain.Repositories;

public interface IBookRepository : IRepository<Book>
{
    Task<Book?> GetByIsbnAsync(string isbn);
    Task<List<Book>> SearchAsync(string? title, string? author);
}

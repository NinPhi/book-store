﻿using Domain.Entities;
using Domain.Shared;

namespace Domain.Repositories;

public interface IBookRepository : IRepository<Book>
{
    Task<Book?> GetByTitleAsync(string title);
}

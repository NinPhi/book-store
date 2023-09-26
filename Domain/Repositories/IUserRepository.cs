using Domain.Entities;
using Domain.Shared;

namespace Domain.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByUsernameAsync(string username);
    Task<bool> ExistsAsync(string username);
}

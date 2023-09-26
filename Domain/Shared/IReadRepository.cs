namespace Domain.Shared;

public interface IReadRepository<TEntity>
{
    Task<TEntity?> GetByIdAsync(long id);
    Task<List<TEntity>> GetAllAsync();
}

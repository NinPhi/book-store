namespace Domain.Shared;

public interface IReadRepository<TEntity>
{
    Task<TEntity?> GetById(long id);
    Task<List<TEntity>> GetAll();
}

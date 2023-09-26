namespace Domain.Shared;

public interface IWriteRepository<TEntity>
{
    Task<TEntity> Create(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
}

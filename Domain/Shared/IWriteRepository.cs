namespace Domain.Shared;

public interface IWriteRepository<TEntity>
{
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
}

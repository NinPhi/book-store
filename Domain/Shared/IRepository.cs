namespace Domain.Shared;

public interface IRepository<TEntity>
    : IReadRepository<TEntity>, IWriteRepository<TEntity>
{
}

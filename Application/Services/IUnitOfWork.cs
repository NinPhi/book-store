namespace Application.Services;

public interface IUnitOfWork
{
    Task CommitAsync();
}

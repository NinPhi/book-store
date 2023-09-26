namespace Application.Shared;

public interface IPasswordManager
{
    string Hash(string password);
    Task<bool> VerifyAsync(string username, string password);
}

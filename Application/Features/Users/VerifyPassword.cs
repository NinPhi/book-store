using Application.Shared;

namespace Application.Features.Users;

public record VerifyPasswordQuery : IRequest<bool>
{
    public required string Username { get; init; }
    public required string Password { get; init; }
}

internal class VerifyPasswordQueryHandler
    : IRequestHandler<VerifyPasswordQuery, bool>
{
    private readonly IPasswordManager _passwordManager;

    public VerifyPasswordQueryHandler(IPasswordManager passwordManager)
    {
        _passwordManager = passwordManager;
    }

    public Task<bool> Handle(VerifyPasswordQuery request, CancellationToken cancellationToken)
    {
        return _passwordManager.VerifyAsync(request.Username, request.Password);
    }
}
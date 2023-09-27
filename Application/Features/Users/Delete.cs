namespace Application.Features.Users;

public record DeleteUserCommand : IRequest
{
    public required string Username { get; init; }
}

internal class DeleteUserCommandHandler
    : IRequestHandler<DeleteUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _uow;

    public DeleteUserCommandHandler(IUserRepository userRepository, IUnitOfWork uow)
    {
        _userRepository = userRepository;
        _uow = uow;
    }

    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByUsernameAsync(request.Username);
        if (user == null) return;

        _userRepository.Delete(user);
        await _uow.CommitAsync();
    }
}
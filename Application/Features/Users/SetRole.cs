using Domain.Enums;

namespace Application.Features.Users;

public record SetUserRoleCommand : IRequest
{
    public required string Username { get; init; }
    public required UserRole Role { get; init; }
}

internal class SetUserRoleCommandHandler
    : IRequestHandler<SetUserRoleCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _uow;

    public SetUserRoleCommandHandler(IUserRepository userRepository, IUnitOfWork uow)
    {
        _userRepository = userRepository;
        _uow = uow;
    }

    public async Task Handle(
        SetUserRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByUsernameAsync(request.Username)
            ?? throw new Exception("User with the specified username doesn't exist.");

        if (!Enum.IsDefined(request.Role)) throw new Exception("Unknown role was specified for the user.");

        user.Role = request.Role;

        _userRepository.Update(user);
        await _uow.CommitAsync();
    }
}

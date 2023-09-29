using Domain.Enums;
using FluentResults;

namespace Application.Features.Users;

public record AddUserCommand : IRequest<Result<UserDto>>
{
    public required string Username { get; init; }
    public required string Password { get; init; }
    public required UserRole Role { get; init; }

    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required DateTime Dob { get; init; }
    public required string Address { get; init; }
}

internal class AddUserCommandHandler
    : IRequestHandler<AddUserCommand, Result<UserDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _uow;
    private readonly IPasswordManager _passwordManager;

    public AddUserCommandHandler(IUserRepository userRepository, IUnitOfWork uow, IPasswordManager passwordManager)
    {
        _userRepository = userRepository;
        _uow = uow;
        _passwordManager = passwordManager;
    }

    public async Task<Result<UserDto>> Handle(
        AddUserCommand request, CancellationToken cancellationToken)
    {
        List<Error> errors = new();

        var userExists = await _userRepository.ExistsAsync(request.Username);
        if (userExists)
            errors.Add(new($"User with username {request.Username} already exists."));

        if (!Enum.IsDefined(request.Role))
            errors.Add(new("Unknown role was specified for the user."));

        if (errors.Any()) return Result.Fail(errors);

        var passwordHash = _passwordManager.Hash(request.Password);

        var user = new User
        {
            Username = request.Username,
            PasswordHash = passwordHash,
            Role = request.Role,

            Profile = request.Adapt<UserProfile>(),
        };

        _userRepository.Add(user);
        await _uow.CommitAsync();

        var response = user.Adapt<UserDto>();
        return response;
    }
}
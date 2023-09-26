using Application.Contracts;
using Application.Shared;
using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using Domain.Shared;
using Mapster;

namespace Application.Features.Users;

public record AddUserCommand : IRequest<UserDto>
{
    public required string Username { get; init; }
    public required string Password { get; init; }
    public required UserRole Role { get; init; }
}

internal class AddUserCommandHandler
    : IRequestHandler<AddUserCommand, UserDto>
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

    public async Task<UserDto> Handle(
        AddUserCommand request, CancellationToken cancellationToken)
    {
        var userExists = await _userRepository.ExistsAsync(request.Username);
        if (userExists) throw new Exception();

        var passwordHash = _passwordManager.Hash(request.Password);

        if (!Enum.IsDefined(request.Role)) throw new Exception();

        var user = new User
        {
            Username = request.Username,
            PasswordHash = passwordHash,
            Role = request.Role,
        };

        _userRepository.Add(user);
        await _uow.CommitAsync();

        var response = user.Adapt<UserDto>();
        return response;
    }
}
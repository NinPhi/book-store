using Application.Contracts;
using Domain.Repositories;
using Mapster;

namespace Application.Features.Users;

public record GetByUsernameQuery : IRequest<UserDto?>
{
    public required string Username { get; init; }
}

internal class GetByUsernameQueryHandler
    : IRequestHandler<GetByUsernameQuery, UserDto?>
{
    private readonly IUserRepository _userRepository;

    public GetByUsernameQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto?> Handle(
        GetByUsernameQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByUsernameAsync(request.Username);

        var response = user.Adapt<UserDto?>();

        return response;
    }
}

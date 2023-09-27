namespace Application.Features.Users;

public record GetUserByUsernameQuery : IRequest<UserDto?>
{
    public required string Username { get; init; }
}

internal class GetUserByUsernameQueryHandler
    : IRequestHandler<GetUserByUsernameQuery, UserDto?>
{
    private readonly IUserRepository _userRepository;

    public GetUserByUsernameQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto?> Handle(
        GetUserByUsernameQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByUsernameAsync(request.Username);

        var response = user.Adapt<UserDto?>();

        return response;
    }
}

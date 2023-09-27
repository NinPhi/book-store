namespace Application.Features.Users;

public record GetAllUsersQuery : IRequest<List<UserDto>> { }

internal class GetAllUsersQueryHandler
    : IRequestHandler<GetAllUsersQuery, List<UserDto>>
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<UserDto>> Handle(
        GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync();

        var response = users.Adapt<List<UserDto>>();

        return response;
    }
}

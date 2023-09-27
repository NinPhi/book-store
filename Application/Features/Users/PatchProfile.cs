namespace Application.Features.Users;

public record PatchUserProfileCommand : IRequest
{
    public required string Username { get; init; }

    public required PatchUserProfileProps Props { get; init; }
}

public record PatchUserProfileProps : IRequest
{
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? Address { get; init; }
    public DateTime? Dob { get; init; }
}

internal class PatchUserProfileCommandHandler
    : IRequestHandler<PatchUserProfileCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _uow;

    public PatchUserProfileCommandHandler(IUserRepository userRepository, IUnitOfWork uow)
    {
        _userRepository = userRepository;
        _uow = uow;
    }

    public async Task Handle(
        PatchUserProfileCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByUsernameAsync(request.Username)
            ?? throw new Exception("User with the specified username doesn't exist.");

        if (!string.IsNullOrWhiteSpace(request.Props.FirstName))
            user.Profile.FirstName = request.Props.FirstName!;

        if (!string.IsNullOrWhiteSpace(request.Props.LastName))
            user.Profile.LastName = request.Props.LastName!;

        if (!string.IsNullOrWhiteSpace(request.Props.Address))
            user.Profile.Address = request.Props.Address!;

        if (request.Props.Dob.HasValue)
            user.Profile.Dob = request.Props.Dob.Value;

        _userRepository.Update(user);
        await _uow.CommitAsync();
    }
}

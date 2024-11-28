using DocumentEditor.Application.Interfaces;
using DocumentEditor.Application.Interfaces.Auth;
using DocumentEditor.Core.Models;


namespace DocumentEditor.Application.Services;

public class UsersService
{
    private readonly IUsersRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;

    public UsersService(IUsersRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtProvider jwtProvider)
    {
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
        _userRepository = userRepository;
    }

    public async Task<Guid> Register(string userName, string email, string password, string avatar)
    {
        var hashedPasseord = _passwordHasher.Generate(password);

        var user = User.Create(Guid.NewGuid(), email, userName, hashedPasseord, avatar, "", DateTime.Now).User;

        await _userRepository.Add(user);

        return user.Id;
    }

    public async Task<string> Login(string email, string password)
    {
        var user = await _userRepository.GetByEmail(email);

        var result = _passwordHasher.Verify(password, user.PasswordHash);

        if (result == false)
        {
            throw new Exception("failed login");
        }

        var token = _jwtProvider.GenerateToken(user);

        return token;
    }
}

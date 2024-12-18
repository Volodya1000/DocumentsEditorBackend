using DocumentEditor.Application.Interfaces;
using DocumentEditor.Application.Interfaces.Auth;
using DocumentEditor.Core.Models;
using System.Text.RegularExpressions;


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

    public async Task<(string,Guid)> Register(string userName, string email, string password, string avatar)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(password) || !IsValidPassword(password))
            {
                throw new Exception("Пароль должен содержать не менее 8 символов и включать хотя бы одну заглавную букву, одну строчную букву, одну цифру и один специальный символ.");
            }


            var hashedPassword = _passwordHasher.Generate(password);
            var userCreationResult = UserDTO.Create(Guid.NewGuid(), email, userName, hashedPassword, avatar, "", DateTime.Now);

            if (userCreationResult.User == null)
            {
                throw new Exception(userCreationResult.Error);
            }

            await _userRepository.Add(userCreationResult.User);
            string token = _jwtProvider.GenerateToken(userCreationResult.User);
            return (token,userCreationResult.User.Id);
        }
        catch (Exception ex)
        {
            throw new Exception($"{ex.Message}");
        }
    }

    public async Task<(string,Guid)> Login(string email, string password)
    {
        var user = await _userRepository.GetByEmail(email);

        var result = _passwordHasher.Verify(password, user.PasswordHash);

        if (result == false)
        {
            throw new Exception("failed login");
        }

        var token = _jwtProvider.GenerateToken(user);

        return (token, user.Id);
    }

    private static bool IsValidPassword(string password)
    {
        return password.Length >= 8 &&
               Regex.IsMatch(password, @"[A-Z]") &&
               Regex.IsMatch(password, @"[a-z]") &&
               Regex.IsMatch(password, @"[0-9]") &&
               Regex.IsMatch(password, @"[\W_]");
    }
}

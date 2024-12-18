using DocumentEditor.Core.Models;

namespace DocumentEditor.Application.Interfaces.Auth
{
    public interface IJwtProvider
    {
        string GenerateToken(UserDTO user);
    }
}

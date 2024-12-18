using DocumentEditor.Core.Models;

namespace DocumentEditor.Application.Interfaces;

public interface IUsersRepository
{
    public Task Add(UserDTO user);

    public Task<UserDTO> GetByEmail(string email);

}

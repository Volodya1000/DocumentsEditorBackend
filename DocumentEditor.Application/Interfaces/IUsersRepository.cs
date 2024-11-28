using DocumentEditor.Core.Models;

namespace DocumentEditor.Application.Interfaces;

public interface IUsersRepository
{
    public Task Add(User user);

    public Task<User> GetByEmail(string email);

}

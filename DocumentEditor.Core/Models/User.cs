namespace DocumentEditor.Core.Models;

public class User
{
    // Поля класса
    public Guid Id { get; }
    public string Email { get; }
    public string UserName { get; }
    public string PasswordHash { get; }
    public string Avatar { get; }
    public string ContactInfo { get; }
    public DateTime BirthDate { get; }
    //public ICollection<DocumentEntity> Documents { get; }

    // Приватный конструктор
    private User(Guid id, string email, string userName, string passwordHash, string avatar, string contactInfo, DateTime birthDate)
    {
        Id = id;
        Email = email;
        UserName = userName;
        PasswordHash = passwordHash;
        Avatar = avatar;
        ContactInfo = contactInfo;
        BirthDate = birthDate;
        //Documents = new List<DocumentEntity>();
    }

    public static (User? User, string Error) Create(Guid id, string email, string userName, string passwordHash, string avatar, string contactInfo, DateTime birthDate)
    {
        var error = string.Empty;

        if (string.IsNullOrWhiteSpace(email))
        {
            error = "Email cannot be empty.";
            return (null, error);
        }

        if (string.IsNullOrWhiteSpace(userName))
        {
            error = "UserName cannot be empty.";
            return (null, error);
        }

        if (string.IsNullOrWhiteSpace(passwordHash))
        {
            error = "Password hash cannot be empty.";
            return (null, error);
        }

        if (avatar == null || avatar.Length == 0)
        {
            error = "Avatar cannot be empty.";
            return (null, error);
        }

        var user = new User(id, email, userName, passwordHash, avatar, contactInfo, birthDate);

        return (user, error);
    }
}

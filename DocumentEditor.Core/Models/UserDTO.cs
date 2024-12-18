using System.Text.RegularExpressions;

namespace DocumentEditor.Core.Models;

public class UserDTO
{
    public Guid Id { get; }
    public string Email { get; }
    public string UserName { get; }
    public string PasswordHash { get; }
    public string Avatar { get; }
    public string ContactInfo { get; }
    public DateTime BirthDate { get; }

    private UserDTO(Guid id, string email, string userName, string passwordHash, string avatar, string contactInfo, DateTime birthDate)
    {
        Id = id;
        Email = email;
        UserName = userName;
        PasswordHash = passwordHash;
        Avatar = avatar;
        ContactInfo = contactInfo;
        BirthDate = birthDate;
    }

    public static (UserDTO? User, string Error) Create(Guid id, string email, string userName, string passwordHash, string avatar, string contactInfo, DateTime birthDate)
    {
        var error = ValidateInput(email, userName, passwordHash);
        if (!string.IsNullOrEmpty(error))
        {
            return (null, error);
        }

        var user = new UserDTO(id, email, userName, passwordHash, avatar, contactInfo, birthDate);
        return (user, string.Empty);
    }

    private static string ValidateInput(string email, string userName, string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(email) || !IsValidEmail(email))
        {
            return "Некорректный адрес электронной почты.";
        }

        if (string.IsNullOrWhiteSpace(userName))
        {
            return "Имя пользователя не может быть пустым.";
        }

       

        return string.Empty;
    }

    

    private static bool IsValidEmail(string email)
    {
        var emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        return Regex.IsMatch(email, emailPattern);
    }
  

   
}


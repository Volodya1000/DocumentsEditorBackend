using DocumentsEditorModel.Entities;
using System.Text.Json.Serialization;

namespace DocumentsEditorModel;

public class UserEntity
{
    public Guid Id {  get; set; }

    public string UserName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public string Avatar { get; set; } = string.Empty; // Хранение аватарки как массив байтов

    public string ContactInfo { get; set; } =string.Empty;

    public DateTime BirthDate { get; set; }
    [JsonIgnore]

    public ICollection<DocumentEntity> Documents { get; set; } = [];  

    public ICollection<UserRoleInDocumentEntity> UserRoleInDocuments { get; set; } = [];

}

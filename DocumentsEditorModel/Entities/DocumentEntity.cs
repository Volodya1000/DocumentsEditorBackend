using DocumentsEditorModel.Entities;
using System.Text.Json.Serialization;

namespace DocumentsEditorModel;

public class DocumentEntity
{
    public Guid Id { get; set; }

    public string Content { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;


    [JsonIgnore]

    public ICollection<UserEntity> Users { get; set; } = [];

    public ICollection<UserRoleInDocumentEntity> UserRoleInDocuments { get; set; } = [];
}

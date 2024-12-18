namespace DocumentsEditorModel.Entities;

public class UserRoleInDocumentEntity
{
    public Guid UserId { get; set; }
    public UserEntity User { get; set; }

    public Guid DocumentId { get; set; }
    public DocumentEntity Document { get; set; }

    public Guid RoleId { get; set; }
    public RoleEntity Role { get; set; }
}

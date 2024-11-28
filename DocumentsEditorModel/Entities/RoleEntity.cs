
namespace DocumentsEditorModel.Entities;

public class RoleEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public ICollection<PermissionEntity> Permissions { get; set; } = [];

    public ICollection<UserRoleInDocumentEntity> UserRoleInDocuments { get; set; } = [];
}

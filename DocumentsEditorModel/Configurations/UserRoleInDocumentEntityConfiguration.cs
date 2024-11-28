using DocumentsEditorModel.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DocumentsEditorModel.Configurations;

public class UserRoleInDocumentEntityConfiguration : IEntityTypeConfiguration<UserRoleInDocumentEntity>
{
    public void Configure(EntityTypeBuilder<UserRoleInDocumentEntity> builder)
    {
        builder.HasKey(urd => new { urd.UserId, urd.DocumentId, urd.RoleId });

        builder.HasOne(urd => urd.User)
            .WithMany(u => u.UserRoleInDocuments)
            .HasForeignKey(urd => urd.UserId);

        builder.HasOne(urd => urd.Document)
            .WithMany(d => d.UserRoleInDocuments)
            .HasForeignKey(urd => urd.DocumentId);

        builder.HasOne(urd => urd.Role)
            .WithMany(r => r.UserRoleInDocuments)
            .HasForeignKey(urd => urd.RoleId);
    }
}

using DocumentsEditorModel.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocumentsEditorModel.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<RoleEntity>
{
    public void Configure(EntityTypeBuilder<RoleEntity> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Name)
            .IsRequired();

        builder.HasMany(r => r.Permissions)
            .WithMany(p => p.Roles);

        builder.HasMany(r => r.UserRoleInDocuments)
            .WithOne(urd => urd.Role);

    }
}

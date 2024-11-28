using DocumentsEditorModel.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocumentsEditorModel.Configurations;

public class DocumentConfiguration : IEntityTypeConfiguration<DocumentEntity>
{
    public void Configure(EntityTypeBuilder<DocumentEntity> builder)
    {
        builder.HasKey(d => d.Id);

        builder.Property(d => d.Content)
            .IsRequired();

        builder.Property(d => d.Title)
            .IsRequired();

        builder.HasMany(d => d.Users)
            .WithMany(u => u.Documents)
            .UsingEntity<UserRoleInDocumentEntity>(
                j => j
                    .HasOne(urd => urd.User)
                    .WithMany(u => u.UserRoleInDocuments)
                    .HasForeignKey(urd => urd.UserId),
                j => j
                    .HasOne(urd => urd.Document)
                    .WithMany(d => d.UserRoleInDocuments)
                    .HasForeignKey(urd => urd.DocumentId));

        builder.HasMany(d => d.UserRoleInDocuments)
            .WithOne(urd => urd.Document);
    }
}

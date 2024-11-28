using DocumentsEditorModel.Configurations;
using DocumentsEditorModel.Entities;
using Microsoft.EntityFrameworkCore;

namespace DocumentsEditorModel
{
    public class DocumentEditorContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<DocumentEntity> Documents { get; set; }
        public DbSet<PermissionEntity> Permissions { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }

        public DocumentEditorContext(DbContextOptions<DocumentEditorContext> options)
            : base(options)
        {
            Database.EnsureCreated();
            SeedData(); 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new DocumentConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new PermissionConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        private void SeedData()
        {
            if (!Roles.Any() && !Permissions.Any()) 
            {
                var readPermission = new PermissionEntity { Name = "Read" };
                var editPermission = new PermissionEntity { Name = "Edit" };
                var deletePermission = new PermissionEntity { Name = "Delete" };
                var editPermissionsPermission = new PermissionEntity { Name = "EditPermissions" };

                Permissions.AddRange(readPermission, editPermission, deletePermission, editPermissionsPermission);
                SaveChanges();

                var adminRole = new RoleEntity
                {
                    Name = "Admin",
                    Permissions = new List<PermissionEntity>
                {
                    readPermission,
                    editPermission,
                    deletePermission,
                    editPermissionsPermission
                }
                };

                var editorRole = new RoleEntity
                {
                    Name = "Editor",
                    Permissions = new List<PermissionEntity>
                {
                    readPermission,
                    editPermission
                }
                };

                var viewerRole = new RoleEntity
                {
                    Name = "Viewer",
                    Permissions = new List<PermissionEntity>
                {
                    readPermission
                }
                };

                Roles.AddRange(adminRole, editorRole, viewerRole);
                SaveChanges(); 
            }
        }
    }
}

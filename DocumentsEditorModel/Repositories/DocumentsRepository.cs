using DocumentsEditorModel.Entities;
using Microsoft.EntityFrameworkCore;

namespace DocumentsEditorModel.Systems;

public class DocumentsRepository
{
    DocumentEditorContext _context;

    public DocumentsRepository(DocumentEditorContext context)
    { _context = context; }


    #region DocumentOperations
    public async Task<bool> UserHasPermission(Guid userId, Guid documentId, string permission)
    {
        var userRoles = await _context.Users
            .Where(u => u.Id == userId)
            .Include(u => u.UserRoleInDocuments)
                .ThenInclude(ur => ur.Role)
                .ThenInclude(r => r.Permissions)
            .SelectMany(u => u.UserRoleInDocuments
                .Where(ur => ur.DocumentId == documentId)
                .SelectMany(ur => ur.Role.Permissions.Select(p => p.Name)))
            .ToListAsync();

        return userRoles.Contains(permission);
    }

    public async Task<DocumentEntity> CreateDocument(Guid creatorId, string title)
    {
        var creator = await _context.Users.FindAsync(creatorId);
        if (creator == null)
        {
            throw new InvalidOperationException("Пользователь не найден.");
        }

        var adminRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "Admin");
        if (adminRole == null)
        {
            throw new InvalidOperationException("Роль администратора не найдена.");
        }

        var document = new DocumentEntity
        {
            Title = title,
            Content = "",
            Users = new List<UserEntity> {  }
        };

        _context.Documents.Add(document);
        await _context.SaveChangesAsync(); 

        var userRoleInDocument = new UserRoleInDocumentEntity
        {
            UserId = creator.Id,
            DocumentId = document.Id, 
            RoleId = adminRole.Id,
            User = creator,
            Document = document,
            Role = adminRole
        };
        creator.UserRoleInDocuments.Add(userRoleInDocument);
        document.UserRoleInDocuments.Add(userRoleInDocument);

        await _context.SaveChangesAsync();

        return document;
    }

    public async Task<bool> EditDocument(Guid documentId, string newContent, Guid editorId)
    {
        var document = await _context.Documents.FindAsync(documentId);

        if (document == null || !await UserHasPermission(editorId, documentId, "Edit"))
            return false;

        document.Content = newContent;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<List<DocumentEntity>> GetUserDocuments(Guid userId)
    {
        return await _context.Documents
            .Where(d => d.Users.Any(u => u.Id == userId))
            .ToListAsync();
    }

    public async Task<DocumentEntity?> GetDocument(Guid documentId)
    {
        return await _context.Documents
            .Include(d => d.Users) 
            .Include(d => d.UserRoleInDocuments) 
                .ThenInclude(ur => ur.Role) 
            .FirstOrDefaultAsync(d => d.Id == documentId); 
    }

    public async Task<bool> DeleteDocument(Guid documentId, Guid deleterId)
    {
        var document = await _context.Documents.FindAsync(documentId);

        if (document == null || !await UserHasPermission(deleterId, documentId, "Delete"))
            return false;

        _context.Documents.Remove(document);

        await _context.SaveChangesAsync();

        return true;
    }
    #endregion

    #region RoleOperations
    public async Task<bool> AddUserToDocument(Guid userId, Guid documentId, string roleName, Guid requesterId)
    {
        
        var document = await _context.Documents.FindAsync(documentId);
        if (document == null)
        {
            return false; 
        }

        if (!await UserHasPermission(requesterId, documentId, "EditPermissions"))
        {
            return false; 
        }

        var user = await _context.Users.FindAsync(userId);
        if (user == null)
        {
            return false; 
        }

        var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
        if (role == null)
        {
            return false; 
        }

        var existingRole = user.UserRoleInDocuments.FirstOrDefault(ur => ur.DocumentId == documentId);

        if (existingRole != null)
        {
            existingRole.RoleId = role.Id; 
        }
        else
        {
            
            var userRoleInDocument = new UserRoleInDocumentEntity
            {
                UserId = user.Id,
                DocumentId = document.Id,
                RoleId = role.Id,
                User = user,
                Document = document,
                Role = role
            };

            user.UserRoleInDocuments.Add(userRoleInDocument); 
            document.UserRoleInDocuments.Add(userRoleInDocument); 
        }

        await _context.SaveChangesAsync();
        return true; 
    }

    public async Task<bool> RemoveUserFromDocument(Guid userId, Guid documentId, Guid requesterId)
    {
        
        var document = await _context.Documents.FindAsync(documentId);
        if (document == null)
        {
            return false; 
        }

        if (!await UserHasPermission(requesterId, documentId, "EditPermissions"))
        {
            return false; 
        }

        var user = await _context.Users.Include(u => u.UserRoleInDocuments)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
        {
            return false; 
        }

       
        var userRoleInDocument = user.UserRoleInDocuments.FirstOrDefault(ur => ur.DocumentId == documentId);

        if (userRoleInDocument == null)
        {
            return false; 
        }

        user.UserRoleInDocuments.Remove(userRoleInDocument);
        document.UserRoleInDocuments.Remove(userRoleInDocument);

        await _context.SaveChangesAsync();
        return true; 
    }


    #endregion
}

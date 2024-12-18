using DocumentsEditorModel.Systems;
using DocumentsEditorModel;
using Microsoft.EntityFrameworkCore;
namespace DocumentEditor.Tests;

public class DocumentEditorTests
{
    private DocumentEditorContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<DocumentEditorContext>()
            .UseInMemoryDatabase("TestDatabase")
            .Options;

        return new DocumentEditorContext(options);
    }

    [Fact]
    public async Task CreateUserAndDocument_ShouldCreateUserAndDocument()
    {
        using var context = CreateDbContext();
        var repository = new DocumentsRepository(context); 

        var creatorId = Guid.NewGuid();
        var user = new UserEntity
        {
            Id = creatorId,
            UserName = "testuser",
            Email = "testuser@example.com",
            PasswordHash = "hashedpassword",
            BirthDate = DateTime.UtcNow.AddYears(-25) 
        };

        context.Users.Add(user);
        await context.SaveChangesAsync();

        var documentTitle = "Test Document";
        var document = await repository.CreateDocument(creatorId, documentTitle);

        Assert.NotNull(document);
        Assert.Equal(documentTitle, document.Title);
        Assert.Contains(user, document.Users); 
    }
}
using DocumentsEditorModel.Systems;
using DocumentsEditorModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
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
        // Arrange
        using var context = CreateDbContext();
        var repository = new DocumentsRepository(context); // Предполагается, что у вас есть такой репозиторий

        var creatorId = Guid.NewGuid();
        var user = new UserEntity
        {
            Id = creatorId,
            UserName = "testuser",
            Email = "testuser@example.com",
            PasswordHash = "hashedpassword", // Используйте хэшированный пароль
            BirthDate = DateTime.UtcNow.AddYears(-25) // Пример даты рождения
        };

        // Act: добавляем пользователя в контекст
        context.Users.Add(user);
        await context.SaveChangesAsync();

        // Теперь создаем документ для этого пользователя
        var documentTitle = "Test Document";
        var document = await repository.CreateDocument(creatorId, documentTitle);

        // Assert
        Assert.NotNull(document);
        Assert.Equal(documentTitle, document.Title);
        Assert.Contains(user, document.Users); // Проверяем, что пользователь добавлен в документ
    }
}//using System;
 //using System.Threading.Tasks;
 //using DocumentEditor.Application.Interfaces;
 //using DocumentEditor.Application.Interfaces.Auth;
 //using DocumentEditor.Application.Services;
 //using DocumentEditor.WebApi.Contracts.Users;
 //using DocumentsEditorModel.Services;
 //using DocumentsEditorModel.Systems;
 //using Moq;
 //using Xunit;
 //using System.Net;
 //using System.Net.Http;
 //using System.Text;
 //using System.Threading.Tasks;
 //using DocumentEditor.WebApi;
 //using DocumentEditor.WebApi.Contracts.Documents;
 //using DocumentEditor.WebApi.Contracts.Users;
 //using FluentAssertions;
 //using Microsoft.AspNetCore.Hosting;
 //using Newtonsoft.Json;
 //using Xunit;
 //namespace DocumentEditor.Tests
 //{


//    public class UserDocumentIntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
//    {
//        private readonly HttpClient _client;

//        public UserDocumentIntegrationTests(WebApplicationFactory<Startup> factory)
//        {
//            _client = factory.CreateClient();
//        }

//        [Fact]
//        public async Task RegisterUserAndCreateDocument_ShouldSucceed()
//        {
//            // Arrange: Create a new user
//            var registerRequest = new RegisterUserRequest("testuser", "test@example.com", "password123", "avatar.png");
//            var registerContent = new StringContent(JsonConvert.SerializeObject(registerRequest), Encoding.UTF8, "application/json");

//            // Act: Register the user
//            var registerResponse = await _client.PostAsync("/register", registerContent);
//            registerResponse.StatusCode.Should().Be(HttpStatusCode.OK);

//            var registeredUserId = await registerResponse.Content.ReadAsStringAsync();

//            // Arrange: Create a document for the registered user
//            var createDocumentRequest = new CreateDocumentRequest(Guid.Parse(registeredUserId), "New Document Title");
//            var createDocumentContent = new StringContent(JsonConvert.SerializeObject(createDocumentRequest), Encoding.UTF8, "application/json");

//            // Act: Create the document
//            var createDocumentResponse = await _client.PostAsync("/documents/create", createDocumentContent);

//            // Assert: Verify that the document was created successfully
//            createDocumentResponse.StatusCode.Should().Be(HttpStatusCode.Created);
//        }
//    }
//}
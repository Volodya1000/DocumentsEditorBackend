using DocumentEditor.WebApi.Contracts.Documents;
using  DocumentsEditorModel.Systems;
using Microsoft.AspNetCore.Mvc;

namespace DocumentEditor.WebApi.Endpoints;

public static class DocumentsEndpoints
{
    public static IEndpointRouteBuilder MapDocumentsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("documents/create", CreateDocument);
        app.MapPut("documents/edit/{documentId}", EditDocument);
        app.MapGet("documents/user/{userId}", GetUserDocuments);
        app.MapGet("documents/{documentId}", GetDocument);
        app.MapDelete("documents/delete/{documentId}", DeleteDocument);
        app.MapPost("documents/{documentId}/add-user", AddUserToDocument);
        app.MapDelete("documents/{documentId}/remove-user", RemoveUserFromDocument);

        return app;
    }

    private static async Task<IResult> CreateDocument([FromBody] CreateDocumentRequest request, [FromServices] DocumentsRepository repository)
    {
        var document = await repository.CreateDocument(request.CreatorId, request.Title);
        return Results.Created($"/documents/{document.Id}", document);
    }

    private static async Task<IResult> EditDocument(Guid documentId, [FromBody] EditDocumentRequest request, [FromServices] DocumentsRepository repository)
    {
        var success = await repository.EditDocument(documentId, request.NewContent, request.EditorId);
        return success ? Results.Ok() : Results.NotFound();
    }

    private static async Task<IResult> GetUserDocuments(Guid userId, [FromServices] DocumentsRepository repository)
    {
        var documents = await repository.GetUserDocuments(userId);
        return Results.Ok(documents);
    }

    private static async Task<IResult> GetDocument(Guid documentId, [FromServices] DocumentsRepository repository)
    {
        var document = await repository.GetDocument(documentId);
        return document != null ? Results.Ok(document) : Results.NotFound();
    }

    private static async Task<IResult> DeleteDocument(Guid documentId, Guid deleterId, [FromServices] DocumentsRepository repository)
    {
        var success = await repository.DeleteDocument(documentId, deleterId);
        return success ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AddUserToDocument([FromBody] AddUserToDocumentRequest request, [FromServices] DocumentsRepository repository)
    {
        var success = await repository.AddUserToDocument(request.UserId, request.DocumentId, request.RoleName, request.RequesterId);
        return success ? Results.Ok() : Results.NotFound();
    }

    private static async Task<IResult> RemoveUserFromDocument([FromBody] RemoveUserFromDocumentRequest request, [FromServices] DocumentsRepository repository)
    {
        var success = await repository.RemoveUserFromDocument(request.UserId, request.DocumentId, request.RequesterId);
        return success ? Results.Ok() : Results.NotFound();
    }
}
using DocumentEditor.WebApi.Contracts.Documents;
using  DocumentsEditorModel.Systems;
using Microsoft.AspNetCore.Mvc;

namespace DocumentEditor.WebApi.Endpoints;

public static class DocumentsEndpoints
{
    public static IEndpointRouteBuilder MapDocumentsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("documents/create", CreateDocument);
        app.MapPut("documents/edit/", EditDocument);
        app.MapGet("documents/", GetDocument);
        app.MapGet("documents/all/", GetAllDocuments);
        app.MapGet("documents/notin/", GetUsersWithoutRoleInDocument);
        app.MapDelete("documents/delete/", DeleteDocument);
        app.MapPost("documents/add-user", AddUserToDocument);
        app.MapDelete("documents/remove-user", RemoveUserFromDocument);

        return app;
    }

    private static async Task<IResult> CreateDocument([FromBody] CreateDocumentRequest request, [FromServices] DocumentsRepository repository)
    {
        var document = await repository.CreateDocument(request.CreatorId, request.Title);
        return Results.Created($"/documents/{document.Id}", document.Id);
    }

    private static async Task<IResult> EditDocument( [FromBody] EditDocumentRequest request, [FromServices] DocumentsRepository repository)
    {
        var success = await repository.EditDocument(request.DocumentId, request.NewContent, request.EditorId, request.NewTitle);
        return success ? Results.Ok() : Results.NotFound();
    }

    private static async Task<IResult> GetAllDocuments([FromQuery] Guid userId, [FromServices] DocumentsRepository repository)
    {
         var rezult = await repository.GetAllDocuments(userId);
        return Results.Ok(rezult);
    }

    private static async Task<IResult> GetDocument(Guid documentId, Guid userId, [FromServices] DocumentsRepository repository)
    {
        var document = await repository.GetDocument(documentId, userId);
        return document != null ? Results.Ok(document) : Results.NotFound();
    }

    private static async Task<IResult> DeleteDocument([FromQuery] Guid documentId, [FromQuery] Guid deleterId, [FromServices] DocumentsRepository repository)
    {
        var success = await repository.DeleteDocument(documentId, deleterId);
        return success ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> GetUsersWithoutRoleInDocument([FromQuery] Guid documentId, [FromServices] DocumentsRepository repository)
    {
         var rezult = await repository.GetUsersWithoutRoleInDocument(documentId);
        return Results.Ok(rezult);
    }
    

    private static async Task<IResult> AddUserToDocument([FromBody] AddUserToDocumentRequest request, [FromServices] DocumentsRepository repository)
    {
        var success = await repository.AddUserToDocument(request.UserId, request.DocumentId, request.RoleName, request.RequesterId);
        return success ? Results.Ok() : Results.NotFound();
    }

    public static async Task<IResult> RemoveUserFromDocument(
    [FromQuery] Guid userId,
    [FromQuery] Guid documentId,
    [FromQuery] Guid requesterId,
    [FromServices] DocumentsRepository repository)
    {
        var success = await repository.RemoveUserFromDocument(userId, documentId, requesterId);
        return success ? Results.Ok() : Results.NotFound();
    }
}
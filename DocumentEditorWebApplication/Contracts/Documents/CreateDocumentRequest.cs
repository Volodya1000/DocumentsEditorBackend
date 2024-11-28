namespace DocumentEditor.WebApi.Contracts.Documents
{
    public record CreateDocumentRequest(
    Guid CreatorId,
    string Title);
}

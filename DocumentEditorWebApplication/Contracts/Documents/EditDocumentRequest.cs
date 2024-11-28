namespace DocumentEditor.WebApi.Contracts.Documents;

public record EditDocumentRequest(
string NewContent,
Guid EditorId);

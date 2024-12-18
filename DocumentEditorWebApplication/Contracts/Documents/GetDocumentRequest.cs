namespace DocumentEditor.WebApi.Contracts.Documents;

public record GetDocumentRequest(
Guid DocumentId,
Guid UserId);

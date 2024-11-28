namespace DocumentEditor.WebApi.Contracts.Documents;

public record RemoveUserFromDocumentRequest(
Guid UserId,
Guid DocumentId,
Guid RequesterId);

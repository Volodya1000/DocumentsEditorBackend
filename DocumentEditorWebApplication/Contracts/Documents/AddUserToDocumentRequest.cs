namespace DocumentEditor.WebApi.Contracts.Documents;

public record AddUserToDocumentRequest(
    Guid UserId,
    Guid DocumentId,
    string RoleName,
    Guid RequesterId);

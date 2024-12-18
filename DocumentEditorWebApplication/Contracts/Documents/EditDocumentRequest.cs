using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace DocumentEditor.WebApi.Contracts.Documents;

public record EditDocumentRequest(
[Required] Guid DocumentId,
[Required] Guid EditorId,
[Required] string NewContent,
[Required] string NewTitle
);

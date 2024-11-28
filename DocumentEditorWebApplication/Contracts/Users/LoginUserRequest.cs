using System.ComponentModel.DataAnnotations;

namespace DocumentEditor.WebApi.Contracts.Users;

public record LoginUserRequest(
[Required] string Email, 
[Required] string Password);

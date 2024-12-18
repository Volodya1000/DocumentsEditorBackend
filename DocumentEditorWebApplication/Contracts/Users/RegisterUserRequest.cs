﻿using System.ComponentModel.DataAnnotations;

namespace DocumentEditor.WebApi.Contracts.Users;

public record RegisterUserRequest(
[Required] string UserName,
[Required] string Email,
[Required] string Password);


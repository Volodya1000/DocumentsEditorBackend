﻿using DocumentEditor.Application.Interfaces.Auth;
using Microsoft.Extensions.Options;
using DocumentEditor.Core.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;

namespace DocumentEditor.Infrastructure;

public class JwtProvider(IOptions<JwtOptions> options) : IJwtProvider
{
    private readonly JwtOptions _options = options.Value;

    public string GenerateToken(UserDTO user)
    {
        Claim[] claims = [new("userId", user.Id.ToString())];

        //Создание алгоритма кодирования
        var signingCredentails =
            new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_options.SecretKey)), SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredentails,
            expires: DateTime.UtcNow.AddHours(_options.ExpitesHours));

        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
        return tokenValue;
    }
}

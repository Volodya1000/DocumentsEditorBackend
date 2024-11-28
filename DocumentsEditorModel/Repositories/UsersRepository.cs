﻿using Microsoft.EntityFrameworkCore;
using DocumentEditor.Core.Models;
using DocumentEditor.Application.Interfaces;

namespace DocumentsEditorModel.Services;

public class UsersRepository:IUsersRepository
{
    DocumentEditorContext _context;

    public UsersRepository(DocumentEditorContext context)
    {  _context = context; }

    public async Task Add(User user)
    {
        var userEntity= new UserEntity()
        {
            Id = user.Id,
            Email = user.Email,
            UserName = user.UserName,
            PasswordHash = user.PasswordHash,
            Avatar = user.Avatar,
            ContactInfo = user.ContactInfo,
            BirthDate = user.BirthDate
        };
        await _context.Users.AddAsync(userEntity);
        await _context.SaveChangesAsync();
    }

    public async Task<User> GetByEmail(string email)
    {
        var userEntity = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email) ??throw new Exception();
        

        var user = User.Create(
            userEntity.Id,
            userEntity.Email,
            userEntity.UserName,
            userEntity.PasswordHash,
            userEntity.Avatar,
            userEntity.ContactInfo,
            userEntity.BirthDate
        ).User;

        return user; 
    }


    public void EditBirthDate(Guid Id, DateTime NewBirthDate)
    {

    }

    public void EditContactIndo(Guid Id, string NewContactInfo)
    {

    }

    public void EditAvatar(Guid Id)
    {

    }
}
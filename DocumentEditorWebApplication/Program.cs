using DocumentEditor.Application.Interfaces;
using DocumentEditor.Application.Interfaces.Auth;
using DocumentEditor.Application.Services;
using DocumentEditor.Infrastructure;
using DocumentEditor.WebApi.Extensions;
using DocumentsEditorModel;
using DocumentsEditorModel.Services;
using DocumentsEditorModel.Systems;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;



services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    options.SerializerOptions.WriteIndented = true; // Optional: for pretty-printing JSON
});


services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));

services.AddEndpointsApiExplorer();

services.AddSwaggerGen();

services.AddDbContext<DocumentEditorContext>(options =>
    options.UseSqlite("Data Source=DocumentEditorDataBase.db"));

services.AddScoped<IUsersRepository,UsersRepository>();

services.AddScoped<UsersService>();
services.AddScoped<DocumentsRepository>();


services.AddScoped<IJwtProvider, JwtProvider>();
services.AddScoped<IPasswordHasher, PasswordHasher>();

services.AddApiAuthentication(configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    //app.UseSwaggerUI();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Document Editor API V1");
        //c.RoutePrefix = string.Empty; // Чтобы Swagger UI был доступен по корневому URL
    });
}

app.UseHttpsRedirection();


//app.UseCors("AllowAllOrigins");
app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly=HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

app.UseAuthentication();


app.AddMappedEndpoints();



app.Run();

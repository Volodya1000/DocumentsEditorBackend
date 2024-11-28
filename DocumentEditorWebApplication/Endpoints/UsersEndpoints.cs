using DocumentEditor.Application.Services;
using DocumentEditor.WebApi.Contracts.Users;

namespace DocumentEditor.WebApi.Endpoints;

public static class UsersEndpoints
{
    public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("register", Register);
        app.MapPost("login", Login);

        return app;
    }

    private static async Task<IResult> Register(
        RegisterUserRequest  request,UsersService UserService)
    {
        Guid newUserId=await UserService.Register(request.UserName, request.Email, request.Password, request.Avatar);
        return Results.Ok(newUserId);
    }

    private static async Task<IResult> Login(LoginUserRequest request,
        UsersService UserService,
        HttpContext context)
    {
        var token = await UserService.Login(request.Email, request.Password);

        context.Response.Cookies.Append("login-cookies",token);

        return Results.Ok(token);
    }
}

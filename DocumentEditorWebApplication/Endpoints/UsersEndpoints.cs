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

    private static async Task<IResult> Register(RegisterUserRequest request, UsersService userService)
    {
        try
        {
            var (token, newUserId) = await userService.Register(request.UserName, request.Email, request.Password, "");
            var result = new { Token = token, NewUserId = newUserId };
            return Results.Ok(result);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }

    private static async Task<IResult> Login(LoginUserRequest request,
        UsersService UserService,
        HttpContext context)
    {
        var (token,id) = await UserService.Login(request.Email, request.Password);

        context.Response.Cookies.Append("login-cookies",token);

        return Results.Ok(new { Token = token, Id = id });
    }
}

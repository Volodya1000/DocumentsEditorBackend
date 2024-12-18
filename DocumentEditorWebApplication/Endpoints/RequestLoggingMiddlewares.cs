namespace DocumentEditor.WebApi.Endpoints;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public RequestLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        Console.WriteLine($"Запрос: {context.Request.Method} {context.Request.Path}");
        Console.WriteLine("Заголовки: " + string.Join(", ", context.Request.Headers));
        await _next(context);
    }
}




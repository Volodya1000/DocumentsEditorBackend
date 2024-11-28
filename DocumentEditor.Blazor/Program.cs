using DocumentEditor.Blazor;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

app.UseCors(cors => cors
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials());

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7222/") });


await builder.Build().RunAsync();

//@using Microsoft.AspNetCore.Components.WebAssembly.Authentication

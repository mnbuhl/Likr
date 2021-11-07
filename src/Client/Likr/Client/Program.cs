using Likr.Client;
using Likr.Client.Handlers;
using Likr.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<CustomAuthorizationMessageHandler>();

builder.Services.AddHttpClient("GatewayApi")
    .AddHttpMessageHandler<CustomAuthorizationMessageHandler>();
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
    .CreateClient("GatewayApi"));

builder.Services.AddTransient<AuthService>();
builder.Services.AddScoped<IHttpService, HttpService>();
builder.Services.AddScoped<IPostService, PostService>();

builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Local", options.ProviderOptions);
    options.AuthenticationPaths.LogOutCallbackPath = "/";
    options.AuthenticationPaths.RemoteRegisterPath = "Account/Register";
});

await builder.Build().RunAsync();
using Likr.Client;
using Likr.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("GatewayApi")
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();
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
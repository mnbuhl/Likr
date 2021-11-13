using Likr.Client;
using Likr.Client.Handlers;
using Likr.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<CustomAuthorizationMessageHandler>();

builder.Services.AddHttpClient("GatewayApi.Auth", client => 
        client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("GatewayUri") + "/api/"))
    .AddHttpMessageHandler<CustomAuthorizationMessageHandler>();

builder.Services.AddHttpClient("GatewayApi.NoAuth", client =>
    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("GatewayUri") + "/api/"));

builder.Services.AddTransient<AuthService>();
builder.Services.AddScoped<IHttpService, HttpService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<ILikeService, LikeService>();

builder.Logging.SetMinimumLevel(builder.HostEnvironment.IsDevelopment() ? LogLevel.Information : LogLevel.None);

builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Local", options.ProviderOptions);
    options.AuthenticationPaths.LogOutCallbackPath = "/";
    options.AuthenticationPaths.RemoteRegisterPath = "Account/Register";
});

await builder.Build().RunAsync();
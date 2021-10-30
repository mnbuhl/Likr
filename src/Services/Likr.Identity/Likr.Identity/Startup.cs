using System;
using System.Linq;
using IdentityServer4;
using IdentityServer4.Models;
using Likr.Identity.Server.Data;
using Likr.Identity.Server.Models;
using Likr.Identity.Server.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Likr.Identity.Server
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _env = env;
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    _configuration.GetConnectionString("DefaultConnection")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentityServer(x =>
                {
                    x.IssuerUri = _configuration.GetValue<string>("ServiceUrl");
                    x.Events.RaiseSuccessEvents = true;
                    x.Events.RaiseFailureEvents = true;
                    x.Events.RaiseErrorEvents = true;
                    x.Authentication.CookieLifetime = TimeSpan.FromHours(2);
                })
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>(opt =>
                {
                    opt.ApiScopes.Add(new ApiScope(IdentityServerConstants.LocalApi.ScopeName));

                    opt.Clients.AddSPA("Likr.Client", spa =>
                    {
                        spa.WithClientId("likr-client");
                        spa.WithRedirectUri(
                            _configuration.GetValue<string>("Urls:BaseUri") + "/authentication/login-callback");
                        spa.WithLogoutRedirectUri(
                            _configuration.GetValue<string>("Urls:BaseUri") + "/authentication/logout-callback");
                        spa.WithScopes("openid", "profile", "IdentityServerApi");
                        spa.WithoutClientSecrets();
                    });

                    var client = opt.Clients.FirstOrDefault(x => x.ClientId == "likr-client");

                    if (client != null)
                    {
                        client.AllowedGrantTypes = new[] { "implicit" };
                        client.AllowedCorsOrigins = _configuration.GetSection("AllowedOrigins").Get<string[]>();
                    }

                    if (_env.IsDevelopment())
                    {
                        opt.Clients.AddSPA("Postman", spa =>
                        {
                            spa.WithClientId("postman");
                            spa.WithRedirectUri("urn:ietf:wg:oauth:2.0:oob");
                            spa.WithoutClientSecrets();
                            spa.WithScopes("openid", "profile", "IdentityServerApi");
                        });
                    }
                })
                .AddDeveloperSigningCredential()
                .AddProfileService<ProfileService>();

            services.AddAuthentication()
                .AddIdentityServerJwt();

            services.AddCors();
            services.AddLocalApiAuthentication();

            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            }

            app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Strict });

            app.UseCors(builder =>
            {
                builder.WithOrigins(_configuration.GetSection("AllowedOrigins").Get<string[]>())
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
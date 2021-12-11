using Microsoft.AspNetCore.Hosting;


[assembly: HostingStartup(typeof(Likr.Identity.Server.Areas.Identity.IdentityHostingStartup))]

namespace Likr.Identity.Server.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((_, _) => { });
        }
    }
}
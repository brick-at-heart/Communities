using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(BrickAtHeart.Communities.Areas.Administration.AdministrationHostingStartup))]
namespace BrickAtHeart.Communities.Areas.Administration
{
    public class AdministrationHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => { });
        }
    }
}

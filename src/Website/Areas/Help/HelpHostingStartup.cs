using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(BrickAtHeart.Communities.Areas.Help.HelpHostingStartup))]
namespace BrickAtHeart.Communities.Areas.Help
{
    public class HelpHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => { });
        }
    }
}

using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(BrickAtHeart.Communities.Areas.Community.CommunityHostingStartup))]
namespace BrickAtHeart.Communities.Areas.Community
{
    public class CommunityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => { });
        }
    }
}

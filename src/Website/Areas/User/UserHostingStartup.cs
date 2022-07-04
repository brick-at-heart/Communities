using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(BrickAtHeart.Communities.Areas.User.UserHostingStartup))]
namespace BrickAtHeart.Communities.Areas.User
{
    public class UserHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => { });
        }
    }
}

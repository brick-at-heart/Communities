using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace BrickAtHeart.Communities
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults( webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .Build()
                .Run();
        }
    }
}
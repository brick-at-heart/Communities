using BrickAtHeart.Communities.Areas.User.Data;
using BrickAtHeart.Communities.Data;
using Microsoft.EntityFrameworkCore;

namespace BrickAtHeart.Communities
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("CommunitiesContextConnection") ?? throw new InvalidOperationException("Connection string 'CommunitiesContextConnection' not found.");

            builder.Services.AddDbContext<CommunitiesContext>(options =>
                options.UseSqlServer(connectionString));;

            builder.Services.AddDefaultIdentity<CommunitiesUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<CommunitiesContext>();;

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();;

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
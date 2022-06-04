using BrickAtHeart.Communities.Areas.User.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BrickAtHeart.Communities.Data;

public class CommunitiesContext : IdentityDbContext<CommunitiesUser>
{
    public CommunitiesContext(DbContextOptions<CommunitiesContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}

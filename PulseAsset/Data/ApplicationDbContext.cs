using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PulseAsset.Models;

namespace PulseAsset.Data;

public class ApplicationDbContext : IdentityDbContext<UserModel>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<LocationModel>().HasData(new LocationModel
        {
            LocationId = -1,
            Name = "Default Location",
            Address = "123 Main St, New York, NY 10001"
        });
        
        builder.Entity<CategoryModel>().HasData(new CategoryModel
        {
            CategoryId = -1,
            Name = "Default Category",
            Description = "This is the default category seeded by the application on setup. Feel free to edit it!"
        });
    }

    public DbSet<AssetModel> Assets { get; set; }
    public DbSet<CategoryModel> Categories { get; set; }
    public DbSet<LocationModel> Locations { get; set; }
    public DbSet<UserModel> Users { get; set; }

}
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
    
    public DbSet<AssetModel> Assets { get; set; }
    public DbSet<CategoryModel> Categories { get; set; }
    public DbSet<LocationModel> Locations { get; set; }
    public DbSet<UserModel> Users { get; set; }

}
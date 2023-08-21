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
}
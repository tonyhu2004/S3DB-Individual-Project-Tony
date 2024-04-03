using Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
        base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Productinformation> ProductInformations { get; set; }
    public DbSet<Review> Reviews { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>()
            .HasIndex(p => p.ID)
            .IsUnique();

        modelBuilder.Entity<Productinformation>()
            .HasIndex(pi => pi.ID)
            .IsUnique();

        modelBuilder.Entity<Review>()
            .HasIndex(r => r.ID)
            .IsUnique();
    }
}
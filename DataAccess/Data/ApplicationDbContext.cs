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
    public DbSet<Chat> Chats { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<ProductInformation> ProductInformations { get; set; }
    public DbSet<Review> Reviews { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>()
            .HasIndex(p => p.Id)
            .IsUnique();        
        
        modelBuilder.Entity<Chat>()
            .HasIndex(c => c.Id)
            .IsUnique();
        
        modelBuilder.Entity<Message>()
            .HasIndex(m => m.Id)
            .IsUnique();

        modelBuilder.Entity<ProductInformation>()
            .HasIndex(pi => pi.Id)
            .IsUnique();

        modelBuilder.Entity<Review>()
            .HasIndex(r => r.Id)
            .IsUnique();
    }
}
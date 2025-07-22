namespace dotnet_api.Infra;

using dotnet_api.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): DbContext(options)
{
    public DbSet<UserModel> Users { get; set; }
    
    public DbSet<CustomerModel> Customers { get; set; }
    
    public DbSet<AddressesModel> Addresses { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User configuration
        modelBuilder.Entity<UserModel>(entity =>
        {
            entity.HasIndex(e => e.Email).IsUnique();
        });
        
        // Customer configuration
        modelBuilder.Entity<CustomerModel>(entity =>
        {
            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasIndex(e => e.Cpf).IsUnique();
        });

        // Address configuration
        modelBuilder.Entity<AddressesModel>(entity =>
        {
            entity.HasOne(a => a.Customer)
                  .WithMany(c => c.Addresses)
                  .HasForeignKey(a => a.CustomerId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
namespace dotnet_api.Infra;

using dotnet_api.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): DbContext(options)
{
    public DbSet<UserModel?> Users { get; set; }
    
    public DbSet<CustomerModel?> Customers { get; set; }
    
    public DbSet<AddressesModel?> Addresses { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserModel>()
            .HasIndex(w => new { w.Email })
            .IsUnique();
        
        modelBuilder.Entity<CustomerModel>()
            .HasIndex(w => new { w.Email, w.Cpf })
            .IsUnique();
    }
}
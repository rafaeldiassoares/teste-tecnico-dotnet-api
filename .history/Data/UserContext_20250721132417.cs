using Microsoft.EntityFrameworkCore;

namespace dotnet_api.Data;

public class UserContext : DbContext
{
  public DbSet<UserModel> Users { get; set; }
}
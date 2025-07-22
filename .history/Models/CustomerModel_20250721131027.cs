namespace dotnet_api.Models;

public class UserModel
{

  public UserModel(string name)
  {
    Name = name;
    Id = Guid.NewGuid();
    Name = name;
    Email = email;
    Password = password;
  }

  public Guid? Id { get; set; }

  public string Name { get; set; }

  public string Email { get; set; }

  public string Password { get; set; }

  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
  public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
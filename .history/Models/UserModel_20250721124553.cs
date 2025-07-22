namespace User.Models;

public class UserModel
{

  public 

  public Guid? Id { get; set; }

  public string Name { get; set; }

  public string Email { get; set; }

  public string Password { get; set; }

  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
  public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
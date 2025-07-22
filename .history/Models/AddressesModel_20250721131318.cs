namespace dotnet_api.Models;

public class AddressesModel
{
  public Guid? Id { get; set; }

  public Guid? CustomerId { get; set; }

  public string Address { get; set; }
  
  public string Number { get; set; }
  
  public string State { get; set; }
  
  public string ZipCode { get; set; }
  
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
  public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}

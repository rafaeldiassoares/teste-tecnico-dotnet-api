namespace dotnet_api.Models;

public class AddressesModel
{
  public Guid? Id { get; set; }

  public Guid? CustomerId { get; set; }

  public string Street { get; set; }
  
  public string City { get; set; }
  
  public string State { get; set; }
  
  public string ZipCode { get; set; }
  
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
  public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet_api.Models;

[Table("addresses")]
public class AddressesModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [Column("customer_id")]
    public int CustomerId { get; set; }

    [Required]
    public string Address { get; set; } = string.Empty;
    
    [Required]
    public string Number { get; set; } = string.Empty;
    
    public string Complement { get; set; } = string.Empty;
    
    [Required]
    [Column("zip_code")]
    public string ZipCode { get; set; } = string.Empty;
    
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation property
    [ForeignKey("CustomerId")]
    public virtual CustomerModel Customer { get; set; } = null!;
}

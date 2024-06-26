using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBD_Final_Project.Entities;

[Table("Clients")]
public class Client
{
    [Key]
    [Column("ClientId")]
    public int ClientId { get; set; }

    [Required]
    [Column("Address")]
    [MaxLength(100)]
    public string Address { get; set; }
    
    [Required]
    [Column("Email")]
    [MaxLength(50)]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [Column("PhoneNumber")]
    [MaxLength(15)]
    [Phone]
    public string PhoneNumber { get; set; }
    
    public IndividualClient IndividualClient { get; set; } = null!;
    public CorporateClient CorporateClient { get; set; } = null!;
}
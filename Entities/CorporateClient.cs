using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBD_Final_Project.Entities;

[Table("Corporate_Client")]
public class CorporateClient
{
    [Key]
    [Column("Corporate_ClientId")]
    public int CorporateClientId { get; set; }
    
    [Column("ClientId")]
    [ForeignKey("Client")]
    public int ClientId { get; set; }
    
    [Required]
    [Column("CompanyName")]
    [MaxLength(100)]
    public string CompanyName { get; set; }
    
    [Required]
    [Column("KRS")]
    [MaxLength(10)]
    public string KRS { get; set; }
    
    public Client Client { get; set; } = null!;
}
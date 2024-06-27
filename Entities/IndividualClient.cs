using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBD_Final_Project.Entities;

[Table("Individual_Client")]
public class IndividualClient
{
    [Key]
    [Column("Individual_ClientId")]
    public int IndividualClientId { get; set; }
    
    [Column("ClientId")]
    [ForeignKey("Client")]
    public int ClientId { get; set; }
    
    [Required]
    [Column("FirstName")]
    [MaxLength(30)]
    public string FirstName { get; set; }
    
    [Required]
    [Column("LastName")]
    [MaxLength(50)]
    public string LastName { get; set; }
    
    [Required]
    [Column("PESEL")]
    [MaxLength(11)]
    public string Pesel { get; set; }

    [Column("DeletedAt")]
    public DateTime? DeletedAt { get; set; }
    
    public Client Client { get; set; } = null!;
}
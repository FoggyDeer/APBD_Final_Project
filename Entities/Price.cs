using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBD_Final_Project.Entities;

[Table("Price")]
public class Price
{
    [Key]
    [ForeignKey("Software")]
    [Column("SoftwareId")]
    public int SoftwareId { get; set; }

    public Software Software { get; set; } = null!;
    
    [Column("AnnualPrice", TypeName = ("decimal(8,2)"))]
    public decimal AnnualPrice { get; set; }
    
    [Column("SubscriptionPrice", TypeName = ("decimal(8,2)"))]
    public decimal SubscriptionPrice { get; set; }
}
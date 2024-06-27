using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APBD_Final_Project.Entities;

[Table("Discount")]
[PrimaryKey("DiscountId", "SoftwareId")]
public class Discount
{
    [Column("DiscountId")]
    public int DiscountId { get; set; }
    
    [ForeignKey("Software")]
    [Column("SoftwareId")]
    public int SoftwareId { get; set; }
    
    public Software Software { get; set; } = null!;
    
    [Column("Name")]
    [MaxLength(50)]
    public string Name { get; set; }
    
    [Column("Value", TypeName = ("decimal(5, 2)"))]
    [Range(0.0, 100.0)]
    public decimal Value { get; set; }
    
    [Column("Type")]
    public Type Type { get; set; }
    
    [Column("StartDate")]
    public DateTime StartDate { get; set; }
    
    [Column("EndDate")]
    public DateTime EndDate { get; set; }
}

public enum Type
{
    Subscription,
    Purchase
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBD_Final_Project.Entities;

[Table("Software")]
public class Software
{
    [Key]
    [Column("SoftwareId")]
    public int SoftwareId { get; set; }
    
    [Required]
    [Column("Name")]
    [MaxLength(50)]
    public string Name { get; set; }
    
    [Required]
    [Column("Description")]
    [MaxLength(1000)]
    public string Description { get; set; }
    
    [Required]
    [Column("Version")]
    [MaxLength(30)]
    public string Version { get; set; }
    
    [Required]
    [Column("Category")]
    public Category Category { get; set; }
    
    public ICollection<Discount> Discounts { get; set; } = null!;
    public ICollection<Contract> Contracts { get; set; } = null!;
}

public enum Category
{
    Finance,
    Education,
    Health,
    Entertainment,
    Utilities,
    Other
}
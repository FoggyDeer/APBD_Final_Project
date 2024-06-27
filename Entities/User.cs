using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBD_Final_Project.Entities;

[Table("User")]
public class User
{
    [Key]
    [Column("UserId")]
    public int UserId { get; set; }

    [Required]
    [Column("Login")]
    [MaxLength(50)]
    [Index(IsUnique = true)]
    public string Login { get; set; } = null!;
    
    [Required]
    [Column("Password")]
    public string Password { get; set; } = null!;

    [Required] 
    [Column("Role")] 
    [MaxLength(20)]
    public string Role { get; set; } = "User";
}
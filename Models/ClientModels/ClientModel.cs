using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBD_Final_Project.Models.ClientModels;

public class ClientModel
{
    [Required]
    [MaxLength(100)]
    public string Address { get; set; }
    
    [Required]
    [MaxLength(50)]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [MaxLength(15)]
    [Phone]
    [RegularExpression(@"^\+\d+$", ErrorMessage = "The PhoneNumber field must contain one '+' and digits.")]
    public string PhoneNumber { get; set; }
}
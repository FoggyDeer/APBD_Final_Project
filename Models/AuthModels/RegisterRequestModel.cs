using System.ComponentModel.DataAnnotations;

namespace JWT.Models;

public class RegisterRequestModel
{
    [Required]
    [MaxLength(50)]
    public string Login { get; set; }
    
    [Required]
    public string Password { get; set; }
}
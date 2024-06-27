using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace JWT.Models;

public class LoginRequestModel
{
    [Required]
    [MaxLength(50)]
    public string Login { get; set; } = null!;
    
    [Required]
    public string Password { get; set; } = null!;
}
using System.ComponentModel.DataAnnotations;

namespace APBD_Final_Project.Models.ClientModels;

public class CreateIndividualClientRequestModel : ClientModel
{
    [Required]
    [MaxLength(30)]
    public string FirstName { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string LastName { get; set; }
    
    [Required]
    [MaxLength(11)]
    [MinLength(11)]
    [RegularExpression(@"^\d+$", ErrorMessage = "The PESEL field must contain only digits.")]
    public string Pesel { get; set; }
}
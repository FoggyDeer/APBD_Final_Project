using System.ComponentModel.DataAnnotations;

namespace APBD_Final_Project.Models.ClientModels;

public class CreateCorporateClientRequestModel : ClientModel
{
    [Required]
    [MaxLength(100)]
    public string CompanyName { get; set; }
    
    [Required]
    [MaxLength(10)]
    [MinLength(10)]
    [RegularExpression(@"^\d+$", ErrorMessage = "The KRS field must contain only digits.")]
    public string KRS { get; set; }
}
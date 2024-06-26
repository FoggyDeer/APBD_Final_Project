using System.ComponentModel.DataAnnotations;

namespace APBD_Final_Project.Models.ClientModels;

public class UpdateCorporateClientRequestModel : ClientModel
{
    [Required]
    [MaxLength(100)]
    public string CompanyName { get; set; }
}
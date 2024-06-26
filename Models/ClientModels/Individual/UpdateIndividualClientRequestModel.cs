using System.ComponentModel.DataAnnotations;

namespace APBD_Final_Project.Models.ClientModels;

public class UpdateIndividualClientRequestModel : ClientModel
{
    [Required]
    [MaxLength(30)]
    public string FirstName { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string LastName { get; set; }
}
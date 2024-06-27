using System.ComponentModel.DataAnnotations;

namespace APBD_Final_Project.Models.ContractModels;

public class CreateContractRequestModel
{
    [Required]
    public int SoftwareId { get; set; }
    
    [Required]
    public DateTime StartDate { get; set; }
    
    [Required]
    public DateTime EndDate { get; set; }

    [Range(1, 4)] 
    public int SupportPeriodYears { get; set; } = 1;
}
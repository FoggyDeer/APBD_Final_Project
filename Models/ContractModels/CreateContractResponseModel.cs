using System.ComponentModel.DataAnnotations;

namespace APBD_Final_Project.Models.ContractModels;

public class CreateContractResponseModel
{
    
    public int ContractId { get; set; }
    
    public int SoftwareId { get; set; }
    
    public decimal Price { get; set; }

    public int SupportPeriodYears { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
}
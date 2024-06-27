using System.ComponentModel.DataAnnotations;

namespace APBD_Final_Project.Models.ContractModels;

public class CreateInvoiceRequestModel
{
    [Required]
    [Range(0.0, 999999.99)]
    public decimal Amount { get; set; }
    public string? Description { get; set; }
}
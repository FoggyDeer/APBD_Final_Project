using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APBD_Final_Project.Entities;

[Table("Contract")]
public class Contract
{
    [Key]
    [Column("ContractId")]
    public int ContractId { get; set; }
    
    [ForeignKey("Client")]
    [Column("ClientId")]
    public int ClientId { get; set; }
    
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public Client Client { get; set; } = null!;
    
    [ForeignKey("Software")]
    [Column("SoftwareId")]
    public int SoftwareId { get; set; }
    
    public Software Software { get; set; } = null!;
    
    [Column("Price", TypeName = "decimal(8, 2)")]
    public decimal Price { get; set; }
    
    [Column("StartDate")]
    public DateTime StartDate { get; set; }
    
    [Column("EndDate")]
    public DateTime EndDate { get; set; }
    
    [Column("SoftWareUpdatesInfo")]
    [MaxLength(1000)]
    public string SoftWareUpdatesInfo { get; set; }
    
    [Column("PaymentSettlementDate")]
    public DateTime? PaymentSettlementDate { get; set; }
    
    public ICollection<Invoice> Invoices { get; set; } = null!;
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace APBD_Final_Project.Entities;

[Table("Invoice")]
public class Invoice
{
    [Key]
    [Column("InvoiceId")]
    public int InvoiceId { get; set; }
    
    [ForeignKey("Contract")]
    [Column("ContractId")]
    public int ContractId { get; set; }
    
    public Contract Contract { get; set; } = null!;
    
    [ForeignKey("Client")]
    [Column("ClientId")]
    public int ClientId { get; set; }
    
    public Client Client { get; set; } = null!;
    
    [Column("Amount", TypeName = "decimal(8, 2)")]
    public decimal Amount { get; set; }
    
    [Column("Description")]
    [MaxLength(50)]
    public string? Description { get; set; }
    
    [Column("IssueDate")]
    public DateTime IssueDate { get; set; }
}
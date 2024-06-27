using APBD_Final_Project.Entities;
using Microsoft.EntityFrameworkCore;

namespace APBD_Final_Project.DbContexts;

public class ApplicationContext : DbContext
{
    public DbSet<Entities.Client> Clients { get; set; }
    public DbSet<Entities.IndividualClient> IndividualClients { get; set; }
    public DbSet<Entities.CorporateClient> CorporateClients { get; set; }
    public DbSet<Entities.Contract> Contracts { get; set; }
    public DbSet<Entities.Software> Software { get; set; }
    public DbSet<Entities.Discount> Discounts { get; set; }
    public DbSet<Entities.Invoice> Invoices { get; set; }
    public DbSet<Entities.User> Users { get; set; }
    
    ApplicationContext()
    {
    }
    
    public ApplicationContext(DbContextOptions options) : base(options)
    {
    }
}
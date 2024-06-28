using APBD_Final_Project.DbContexts.Abstract;
using APBD_Final_Project.Entities;
using Microsoft.EntityFrameworkCore;

namespace APBD_Final_Project.DbContexts;

public class ApplicationContext : DbContext, IApplicationContext
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<IndividualClient> IndividualClients { get; set; }
    public DbSet<CorporateClient> CorporateClients { get; set; }
    public DbSet<Contract> Contracts { get; set; }
    public DbSet<Software> Software { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Price> Prices { get; set; }
    
    ApplicationContext()
    {
    }
    
    public ApplicationContext(DbContextOptions options) : base(options)
    {
    }
}
using Microsoft.EntityFrameworkCore;

namespace APBD_Final_Project.DbContexts.Abstract;

public interface IApplicationContext
{
    DbSet<Entities.Client> Clients { get; set; }
    DbSet<Entities.IndividualClient> IndividualClients { get; set; }
    DbSet<Entities.CorporateClient> CorporateClients { get; set; }
    DbSet<Entities.Contract> Contracts { get; set; }
    DbSet<Entities.Software> Software { get; set; }
    DbSet<Entities.Discount> Discounts { get; set; }
    DbSet<Entities.Invoice> Invoices { get; set; }
    DbSet<Entities.User> Users { get; set; }
    DbSet<Entities.Price> Prices { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
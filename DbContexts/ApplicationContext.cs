using Microsoft.EntityFrameworkCore;

namespace APBD_Final_Project.DbContexts;

public class ApplicationContext : DbContext
{
    public DbSet<Entities.Client> Clients { get; set; }
    public DbSet<Entities.IndividualClient> IndividualClients { get; set; }
    public DbSet<Entities.CorporateClient> CorporateClients { get; set; }
    
    ApplicationContext()
    {
    }
    
    public ApplicationContext(DbContextOptions options) : base(options)
    {
    }
}
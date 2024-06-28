using APBD_Final_Project.DbContexts.Abstract;
using APBD_Final_Project.Entities;
using APBD_Final_Project.UnitTests.Fakes;
using Microsoft.EntityFrameworkCore;

namespace APBD_Final_Project.DbContexts;

public class FakeApplicationContext : IApplicationContext
{
    public DbSet<Client> Clients { get; set; } = new FakeDbSet<Client>();
    public DbSet<IndividualClient> IndividualClients { get; set; } = new FakeDbSet<IndividualClient>();
    public DbSet<CorporateClient> CorporateClients { get; set; } = new FakeDbSet<CorporateClient>();
    public DbSet<Contract> Contracts { get; set; } = new FakeDbSet<Contract>();
    public DbSet<Software> Software { get; set; } = new FakeDbSet<Software>();
    public DbSet<Discount> Discounts { get; set; } = new FakeDbSet<Discount>();
    public DbSet<Invoice> Invoices { get; set; } = new FakeDbSet<Invoice>();
    public DbSet<User> Users { get; set; } = new FakeDbSet<User>();
    public DbSet<Price> Prices { get; set; } = new FakeDbSet<Price>();
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(0);
    }

    public FakeApplicationContext()
    {
        Software.AddAsync(new()
        {
            SoftwareId = 1,
            Name = "Software1",
            Description = "Description1",
            Version = "1.0",
            Category = Category.Finance,
        });
        Software.AddAsync(new()
        {
            SoftwareId = 2,
            Name = "Software2",
            Description = "Description2",
            Version = "1.0",
            Category = Category.Finance,
        });
        Software.AddAsync(new()
        {
            SoftwareId = 3,
            Name = "Software3",
            Description = "Description3",
            Version = "1.0",
            Category = Category.Finance,
        });
        
        Users.AddAsync(new()
        {
            UserId = 1,
            Login = "Login1",
            Password = "Password1",
            Role = "Client"
        });
        
        Users.AddAsync(new()
        {
            UserId = 2,
            Login = "Login2",
            Password = "Password2",
            Role = "Client"
        });
        
        Clients.AddAsync(new()
        {
            ClientId = 1,
            User = Users.First(u => u.UserId == 1),
            Address = "Address1",
            Email = "Email1",
            PhoneNumber = "PhoneNumber1"
        });
        Clients.AddAsync(new()
        {
            ClientId = 2,
            User = Users.First(u => u.UserId == 2),
            Address = "Address2",
            Email = "Email2",
            PhoneNumber = "PhoneNumber2"
        });
        
        IndividualClients.AddAsync(new()
        {
            ClientId = 1,
            IndividualClientId = 1,
            FirstName = "FirstName1",
            LastName = "LastName1",
            Pesel = "Pesel1"
        });
        Clients.First(c => c.ClientId == 1).IndividualClient = IndividualClients.First(ic => ic.ClientId == 1);
        
        CorporateClients.AddAsync(new()
        {
            ClientId = 2,
            CorporateClientId = 1,
            CompanyName = "CompanyName1",
            KRS = "KRS1"
        });
        Clients.First(c => c.ClientId == 1).CorporateClient = CorporateClients.First(cc => cc.ClientId == 2);
        
        Prices.AddAsync(new()
        {
            SoftwareId = 1,
            Software = Software.First(s => s.SoftwareId == 1),
            AnnualPrice = 1000,
            SubscriptionPrice = 50
        });
        Prices.AddAsync(new()
        {
            SoftwareId = 2,
            Software = Software.First(s => s.SoftwareId == 2),
            AnnualPrice = 750,
            SubscriptionPrice = 50
        });
        Prices.AddAsync(new()
        {
            SoftwareId = 3,
            Software = Software.First(s => s.SoftwareId == 2),
            AnnualPrice = 500,
            SubscriptionPrice = 50
        });
        Discounts.AddAsync(new()
        {
            DiscountId = 1,
            SoftwareId = 1,
            Software = Software.First(s => s.SoftwareId == 1),
            Value = 10,
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(30)
        });
        Discounts.AddAsync(new()
        {
            DiscountId = 3,
            SoftwareId = 1,
            Software = Software.First(s => s.SoftwareId == 2),
            Value = 50,
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(30)
        });
        Discounts.AddAsync(new()
        {
            DiscountId = 2,
            SoftwareId = 2,
            Software = Software.First(s => s.SoftwareId == 2),
            Value = 15,
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(30)
        });
    }
}
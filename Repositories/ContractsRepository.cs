using APBD_Final_Project.DbContexts;
using APBD_Final_Project.DbContexts.Abstract;
using APBD_Final_Project.Entities;
using APBD_Final_Project.Exceptions.ContractExceptions;
using APBD_Final_Project.Models.ContractModels;
using APBD_Final_Project.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace APBD_Final_Project.Repositories;

public class ContractsRepository(IApplicationContext context) : IContractsRepository
{
    public async Task<bool> DoesClientExists(int clientId)
    {
        Client? client = await context.Clients
            .Where(c => c.ClientId == clientId)
            .FirstOrDefaultAsync();
        
        return client != null;
    }

    public async Task<decimal> GetMaxSoftwareDiscount(int softwareId)
    {
        Software? software = await context.Software
            .Include(s => s.Discounts)
            .Where(s => s.SoftwareId == softwareId)
            .FirstOrDefaultAsync();

        return software?.Discounts
            .Where(d => d.EndDate > DateTime.Now)
            .Select(d => (decimal?)d.Value)
            .DefaultIfEmpty(0)
            .Max() ?? 0;
    }

    public async Task<int> CountClientPurchases(int clientId)
    {
        Client? client = await context.Clients
            .Include(c => c.Contracts)
            .Where(c => c.ClientId == clientId)
            .FirstOrDefaultAsync();
        
        return client?.Contracts
            .Count(c => c.PaymentSettlementDate != null) ?? 0;
    }

    public async Task<decimal> GetSoftwareAnnualPrice(int softwareId)
    {
        Software? software = await context.Software
            .Include(s => s.Price)
            .Where(s => s.SoftwareId == softwareId)
            .FirstOrDefaultAsync();
        
        Price? price = software?.Price;
        
        return price?.AnnualPrice ?? 0;
    }

    public async Task<bool> DoesClientHasActiveContract(int clientId)
    {
        Client? client = await context.Clients
            .Include(c => c.Contracts)
            .Where(c => c.ClientId == clientId)
            .FirstOrDefaultAsync();
        
        return client?.Contracts
            .Any(c => c.PaymentSettlementDate == null && c.EndDate > DateTime.Now) ?? false;
    }

    public async Task CreateContract(int clientId, decimal price, CreateContractRequestModel requestModel)
    {
        Contract contract = new()
        {
            ClientId = clientId,
            SoftwareId = requestModel.SoftwareId,
            StartDate = requestModel.StartDate,
            EndDate = requestModel.EndDate,
            Price = price,
            SupportPeriodYears = requestModel.SupportPeriodYears
        };
        await context.Contracts.AddAsync(contract);
        await context.SaveChangesAsync();
    }

    public async Task<Contract?> GetClientsContractById(int clientId,int contractId)
    {
        Client? client = await context.Clients
            .Include(c => c.Contracts)
            .Include(c => c.Invoices)
            .Where(c => c.ClientId == clientId)
            .FirstOrDefaultAsync();
        
        return client?.Contracts.FirstOrDefault(c => c.ContractId == contractId);
    }
    
    public async Task CreateInvoice(int contractId, int clientId, CreateInvoiceRequestModel requestModel)
    {
        Contract? contract = await GetClientsContractById(clientId, contractId);

        if (contract == null)
            throw new ContractNotFoundException(contractId);
        
        decimal totalInvoicesAmount = (contract.Invoices?.Sum(i => i.Amount) ?? 0) + requestModel.Amount;
        
        bool isPaid = totalInvoicesAmount >= contract.Price;
        
        if(isPaid)
            requestModel.Amount -= totalInvoicesAmount - contract.Price;
        
        Invoice invoice = new()
        {
            ContractId = contractId,
            ClientId = clientId,
            Amount = requestModel.Amount,
            Description = requestModel.Description,
            IssueDate = DateTime.Now
        };
        await context.Invoices.AddAsync(invoice);
        
        if(isPaid)
            contract.PaymentSettlementDate = DateTime.Now;
        
        await context.SaveChangesAsync();
    }
}
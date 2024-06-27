using APBD_Final_Project.DbContexts;
using APBD_Final_Project.Entities;
using APBD_Final_Project.Exceptions.ContractExceptions;
using APBD_Final_Project.Models.ContractModels;
using APBD_Final_Project.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace APBD_Final_Project.Repositories;

public class ContractsRepository(ApplicationContext context) : IContractsRepository
{
    public async Task<Software?> GetSoftwareById(int id)
    {
        return await context.Software.FirstOrDefaultAsync(s => s.SoftwareId == id);
    }

    public async Task<decimal> GetMaxSoftwareDiscount(int softwareId)
    {
        return await context.Software
            .Where(s => s.SoftwareId == softwareId)
            .Include(s => s.Discounts)
            .SelectMany(s => s.Discounts)
            .Where(d => d.EndDate > DateTime.Now)
            .Select(d => (decimal?)d.Value)
            .DefaultIfEmpty(0)
            .MaxAsync(d => d) ?? 0;
    }

    public async Task<int> CountClientPurchases(int clientId)
    {
        return await context.Clients
            .Include(c => c.Contracts)
            .Where(c => c.ClientId == clientId)
            .SelectMany(c => c.Contracts)
            .CountAsync(c => c.PaymentSettlementDate != null);
    }

    public async Task<decimal> GetSoftwareAnnualPrice(int softwareId)
    {
        return await context.Software
            .Include(e => e.Price)
            .Where(s => s.SoftwareId == softwareId)
            .Select(s => s.Price.AnnualPrice)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> DoesClientHasActiveContract(int clientId)
    {
        return await context.Clients.Include(c => c.Contracts).Where(c => c.ClientId == clientId).SelectMany(c => c.Contracts).AnyAsync(c => c.PaymentSettlementDate == null && c.EndDate > DateTime.Now);
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
        return await context.Clients
            .Include(c => c.Contracts)
            .Include(c => c.Invoices)
            .Where(c => c.ClientId == clientId)
            .SelectMany(c => c.Contracts)
            .Where(c => c.ContractId == contractId)
            .FirstOrDefaultAsync();
    }
    
    public async Task CreateInvoice(int contractId, int clientId, CreateInvoiceRequestModel requestModel)
    {
        Contract? contract = await GetClientsContractById(clientId, contractId);

        if (contract == null)
            throw new ContractNotFoundException(contractId);
        
        decimal totalInvoicesAmount = contract.Invoices.Sum(i => i.Amount) + requestModel.Amount;
        
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
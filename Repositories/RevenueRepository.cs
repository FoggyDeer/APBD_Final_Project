using APBD_Final_Project.DbContexts;
using APBD_Final_Project.DbContexts.Abstract;
using APBD_Final_Project.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace APBD_Final_Project.Repositories;

public class RevenueRepository (IApplicationContext context) : IRevenueRepository
{
    public async Task<decimal> GetCurrentRevenue()
    {
        decimal? sum = null;
        try
        {
            sum = await context.Contracts
                .Include(e => e.Invoices)
                .Where(c => c.PaymentSettlementDate != null)
                .SelectMany(c => c.Invoices)
                .SumAsync(i => i.Amount);
        }
        catch (ArgumentNullException)
        {
        }
        
        return sum ?? 0;
    }
    
    public async Task<decimal> GetCurrentRevenueForProduct(int productId)
    {
        decimal? sum = null;
        try
        {
            sum = await context.Contracts
                .Include(e => e.Invoices)
                .Where(c => c.SoftwareId == productId && c.PaymentSettlementDate != null)
                .SelectMany(c => c.Invoices)
                .SumAsync(i => i.Amount);
        }
        catch (ArgumentNullException)
        {
        }
        
        return sum ?? 0;
    }
    
    public async Task<decimal> GetForecastRevenue()
    {
        decimal? sum = null;
        try
        {
            sum = await context.Contracts.SumAsync(c => c.Price);
        }
        catch (ArgumentNullException)
        {
        }

        return sum ?? 0;
    }
    
    public async Task<decimal> GetForecastRevenueForProduct(int productId)
    {
        decimal? sum = null;
        try
        {
            sum = await context.Contracts
                .Where(c => c.SoftwareId == productId)
                .SumAsync(c => c.Price);
        }
        catch (ArgumentNullException)
        {
        }
        
        return sum ?? 0;
    }
}
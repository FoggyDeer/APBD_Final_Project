namespace APBD_Final_Project.Repositories.Abstract;

public interface IRevenueRepository
{
    Task<decimal> GetCurrentRevenue();
    Task<decimal> GetCurrentRevenueForProduct(int productId);
    Task<decimal> GetForecastRevenue();
    Task<decimal> GetForecastRevenueForProduct(int productId);
}
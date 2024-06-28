namespace APBD_Final_Project.Services.Abstract;

public interface IRevenueService
{
    Task<string> GetCurrentRevenue(string currency, int? softwareId);
    Task<string> GetForecastRevenue(string currency, int? softwareId);
}
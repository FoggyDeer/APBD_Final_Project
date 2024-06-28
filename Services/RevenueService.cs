using System.Text.Json;
using APBD_Final_Project.Entities;
using APBD_Final_Project.Exceptions.RevenueExceptions;
using APBD_Final_Project.Repositories.Abstract;
using APBD_Final_Project.Services.Abstract;

namespace APBD_Final_Project.Services;

public class RevenueService(IRevenueRepository revenueRepository, ISoftwareRepository softwareRepository) : IRevenueService
{
    private const string Url = "https://v6.exchangerate-api.com/v6/ebb16d4d2fc553e86c21302a/latest/PLN";
    public async Task<string> GetCurrentRevenue(string currency, int? softwareId)
    {
        decimal revenue;
        if (softwareId == null)
        {
            revenue = await revenueRepository.GetCurrentRevenue();
        }
        else
        {
            Software software = await softwareRepository.GetSoftwareById(softwareId.Value);
            revenue = await revenueRepository.GetCurrentRevenueForProduct(software.SoftwareId);
        }

        if(revenue != 0.0m && currency != "PLN")
            return Math.Round(await ConvertToCurrency(currency, revenue), 2) + " " + currency;
        
        return Math.Round(revenue, 2) + " " + currency;
    }
    
    public async Task<string> GetForecastRevenue(string currency, int? softwareId)
    {
        decimal revenue;
        if (softwareId == null)
        {
            revenue = await revenueRepository.GetForecastRevenue();
        }                                      
        else
        {
            Software software = await softwareRepository.GetSoftwareById(softwareId.Value);
            revenue = await revenueRepository.GetForecastRevenueForProduct(software.SoftwareId);
        }
        
        if(revenue != 0.0m && currency != "PLN")
            return Math.Round(await ConvertToCurrency(currency, revenue), 2) + " " + currency;
        
        return Math.Round(revenue, 2) + " " + currency;
    }
    
    private async Task<decimal> ConvertToCurrency(string currency, decimal amount)
    {
        using HttpClient client = new HttpClient();
        client.BaseAddress = new Uri(Url);
        try
        {
            HttpResponseMessage response = await client.GetAsync("");

            if (response.IsSuccessStatusCode) 
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var dataObjects = JsonSerializer.Deserialize<Response>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (dataObjects?.Conversion_Rates == null)
                    throw new CurrencyConvertException();

                if (!dataObjects.Conversion_Rates.ContainsKey(currency))
                    throw new InvalidCurrencyException();
                
                return amount * dataObjects.Conversion_Rates[currency];
            }

            throw new CurrencyConvertException();
        }
        catch (Exception e) when(e is not InvalidCurrencyException)
        {
            throw new CurrencyConvertException();
        }
    }

    private class Response
    {
        public string Result { get; set; }
        public Dictionary<string, decimal> Conversion_Rates { get; set; }
    }
}
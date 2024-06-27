using APBD_Final_Project.Entities;
using APBD_Final_Project.Models.ContractModels;

namespace APBD_Final_Project.Repositories.Abstract;

public interface IContractsRepository
{
    Task<Software?> GetSoftwareById(int id);
    Task<decimal> GetMaxSoftwareDiscount(int softwareId);
    Task<int> CountClientPurchases(int clientId);
    Task<decimal> GetSoftwareAnnualPrice(int softwareId);
    Task<bool> DoesClientHasActiveContract(int clientId);
    Task CreateContract(int clientId, decimal price, CreateContractRequestModel requestModel);
    Task<Contract?> GetClientsContractById(int clientId, int contractId);
    Task CreateInvoice(int contractId, int clientId, CreateInvoiceRequestModel requestModel);
}
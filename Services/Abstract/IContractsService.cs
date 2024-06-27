using APBD_Final_Project.Models.ContractModels;

namespace APBD_Final_Project.Services.Abstract;

public interface IContractsService
{
    Task CreateContract(int clientId, CreateContractRequestModel requestModel);
    Task CreateInvoice(int contractId, int clientId, CreateInvoiceRequestModel requestModel);
}
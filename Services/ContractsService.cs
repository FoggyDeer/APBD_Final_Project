using APBD_Final_Project.Entities;
using APBD_Final_Project.Exceptions.ContractExceptions;
using APBD_Final_Project.Exceptions.SoftwareExceptions;
using APBD_Final_Project.Models.ContractModels;
using APBD_Final_Project.Repositories.Abstract;
using APBD_Final_Project.Services.Abstract;

namespace APBD_Final_Project.Services;

public class ContractsService(IContractsRepository repository) : IContractsService
{
    public async Task CreateContract(int clientId, CreateContractRequestModel requestModel)
    {
        if(await repository.DoesClientHasActiveContract(clientId))
            throw new ActiveContractException();
        
        if(requestModel.StartDate > requestModel.EndDate || requestModel.StartDate < DateTime.Now)
            throw new ContractStartDateException();
        
        double duration = requestModel.EndDate.Subtract(requestModel.StartDate).TotalDays;
        if(duration < 3 || duration > 30)
            throw new InvalidPeriodException();
        
        Software? software = await repository.GetSoftwareById(requestModel.SoftwareId);
        
        if(software == null)
            throw new SoftwareNotFoundException(requestModel.SoftwareId);

        decimal discount = await repository.GetMaxSoftwareDiscount(software.SoftwareId);
        
        bool isLoyal = await repository.CountClientPurchases(clientId) > 1;
        if(isLoyal)
            discount += 5m;
        
        decimal annualPrice = await repository.GetSoftwareAnnualPrice(software.SoftwareId);
        
        decimal price = annualPrice + 1000 * (requestModel.SupportPeriodYears - 1);
        price -= price * discount / 100;
        
        await repository.CreateContract(clientId, price, requestModel);
    }

    public async Task CreateInvoice(int contractId, int clientId, CreateInvoiceRequestModel requestModel)
    {
        Contract? contract = await repository.GetClientsContractById(clientId, contractId);

        if(contract == null)
            throw new ContractNotFoundException(contractId);
        
        if(contract.PaymentSettlementDate != null)
            throw new ContractAlreadyPaidException(contractId);
            
        if(contract.EndDate < DateTime.Now)
            throw new ContractExpiredException(contractId);
        
        await repository.CreateInvoice(contractId, clientId, requestModel);
    }
}
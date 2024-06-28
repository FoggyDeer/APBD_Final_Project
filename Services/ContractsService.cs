using APBD_Final_Project.Entities;
using APBD_Final_Project.Exceptions.ClientsException;
using APBD_Final_Project.Exceptions.ContractExceptions;
using APBD_Final_Project.Models.ContractModels;
using APBD_Final_Project.Repositories.Abstract;
using APBD_Final_Project.Services.Abstract;

namespace APBD_Final_Project.Services;

public class ContractsService(IContractsRepository contractsRepository, ISoftwareRepository softwareRepository) : IContractsService
{
    public async Task CreateContract(int clientId, CreateContractRequestModel requestModel)
    {
        if(!await contractsRepository.DoesClientExists(clientId))
            throw new ClientNotFoundException(clientId);
        
        if(await contractsRepository.DoesClientHasActiveContract(clientId))
            throw new ActiveContractException();
        
        Console.WriteLine(requestModel.StartDate >= requestModel.EndDate);
        Console.WriteLine(requestModel.StartDate < DateTime.Now);
        if(requestModel.StartDate >= requestModel.EndDate || requestModel.StartDate < DateTime.Now)
            throw new ContractStartDateException();
        
        double duration = (requestModel.EndDate - requestModel.StartDate).TotalDays;
        if(duration is < 3 or > 30)
            throw new InvalidPeriodException();
        
        Software software = await softwareRepository.GetSoftwareById(requestModel.SoftwareId);

        decimal discount = await contractsRepository.GetMaxSoftwareDiscount(software.SoftwareId);
        
        bool isLoyal = await contractsRepository.CountClientPurchases(clientId) >= 1;
        if(isLoyal)
            discount += 5m;
        
        decimal annualPrice = await contractsRepository.GetSoftwareAnnualPrice(software.SoftwareId);
        
        decimal price = annualPrice + 1000 * (requestModel.SupportPeriodYears - 1);
        price -= price * discount / 100;
        
        await contractsRepository.CreateContract(clientId, price, requestModel);
    }

    public async Task CreateInvoice(int contractId, int clientId, CreateInvoiceRequestModel requestModel)
    {
        if(!await contractsRepository.DoesClientExists(clientId))
            throw new ClientNotFoundException(clientId);
        
        Contract? contract = await contractsRepository.GetClientsContractById(clientId, contractId);

        if(contract == null)
            throw new ContractNotFoundException(contractId);
        
        if(contract.PaymentSettlementDate != null)
            throw new ContractAlreadyPaidException(contractId);
            
        if(contract.EndDate < DateTime.Now)
            throw new ContractExpiredException(contractId);
        
        await contractsRepository.CreateInvoice(contractId, clientId, requestModel);
    }
}
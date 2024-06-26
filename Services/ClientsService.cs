
using APBD_Final_Project.Entities;
using APBD_Final_Project.Exceptions.ClientsException.Corporate;
using APBD_Final_Project.Exceptions.ClientsException.Individual;
using APBD_Final_Project.Models.ClientModels;
using APBD_Final_Project.Repositories.Abstract;
using APBD_Final_Project.Services.Abstract;

namespace APBD_Final_Project.Services;

public class ClientsService(IClientsRepository clientsRepository) : IClientsService
{
    public async Task AddIndividualClient(CreateIndividualClientRequestModel requestModel)
    {
        if(!await clientsRepository.IsPeselValid(requestModel.Pesel))
        {
            throw new PeselIsNotValidException(requestModel.Pesel);
        }
        
        await clientsRepository.AddIndividualClient(requestModel);
    }

    public async Task<IndividualClientResponseModel> UpdateIndividualClient(UpdateIndividualClientRequestModel requestModel, int clientId)
    {
        if(await clientsRepository.IsIndividualClientDeleted(clientId))
        {
            throw new ClientDeletedException(clientId);
        }
        
        IndividualClient? individualClient = await clientsRepository.UpdateIndividualClient(requestModel, clientId);

        if(individualClient == null)
        {
             throw new Exceptions.ClientsException.Individual.NotFoundException(clientId);
        }
        
        return new IndividualClientResponseModel
        {
            ClientId = individualClient.IndividualClientId,
            FirstName = individualClient.FirstName,
            LastName = individualClient.LastName,
            Pesel = individualClient.Pesel,
            Address = individualClient.Client.Address,
            Email = individualClient.Client.Email,
            PhoneNumber = individualClient.Client.PhoneNumber,
        };
    }

    public async Task DeleteIndividualClient(int clientId)
    {
        if(await clientsRepository.IsIndividualClientDeleted(clientId))
        {
            throw new ClientDeletedException(clientId);
        }
        
        IndividualClient? individualClient = await clientsRepository.DeleteIndividualClient(clientId);
        
        if(individualClient == null)
        {
            throw new Exceptions.ClientsException.Individual.NotFoundException(clientId);
        }
    }

    public async Task AddCorporateClient(CreateCorporateClientRequestModel requestModel)
    {
        if(!await clientsRepository.IsKrsValid(requestModel.KRS))
        {
            throw new KrsIsNotValidException(requestModel.KRS);
        }
        
        await clientsRepository.AddCorporateClient(requestModel);
    }

    public async Task<CorporateClientResponseModel> UpdateCorporateClient(UpdateCorporateClientRequestModel requestModel, int clientId)
    {
        CorporateClient? corporateClient = await clientsRepository.UpdateCorporateClient(requestModel, clientId);
        
        if(corporateClient == null)
        {
            throw new Exceptions.ClientsException.Corporate.NotFoundException(clientId);
        }
        
        return new CorporateClientResponseModel
        {
            ClientId = corporateClient.CorporateClientId,
            CompanyName = corporateClient.CompanyName,
            KRS = corporateClient.KRS,
            Address = corporateClient.Client.Address,
            Email = corporateClient.Client.Email,
            PhoneNumber = corporateClient.Client.PhoneNumber,
        };
    }
}
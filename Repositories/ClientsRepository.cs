using APBD_Final_Project.DbContexts;
using APBD_Final_Project.Entities;
using APBD_Final_Project.Models.ClientModels;
using APBD_Final_Project.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace APBD_Final_Project.Repositories;

public class ClientsRepository(ApplicationContext context) : IClientsRepository
{
    public async Task<bool> IsIndividualClientDeleted(int clientId)
    {
        IndividualClient? client = await context.IndividualClients
            .FirstOrDefaultAsync(c => c.IndividualClientId == clientId);
        
        return client?.DeletedAt != null;
    }

    public async Task<bool> IsPeselValid(string pesel)
    {
        IndividualClient? client = await context.IndividualClients
            .FirstOrDefaultAsync(c => c.Pesel == pesel);
        
        return client == null;
    }
    
    public async Task<bool> IsKrsValid(string krs)
    {
        CorporateClient? client = await context.CorporateClients
            .FirstOrDefaultAsync(c => c.KRS == krs);
        
        return client == null;
    }
    
    public async Task<bool> DoesClientExists(int clientId)
    {
        Client? client = await context.Clients
            .FirstOrDefaultAsync(c => c.ClientId == clientId);
        
        return client != null;
    }

    public async Task AddIndividualClient(int userId, CreateIndividualClientRequestModel requestModel)
    {
        Client client = new Client
        {
            ClientId = userId,
            Address = requestModel.Address,
            Email = requestModel.Email,
            PhoneNumber = requestModel.PhoneNumber,
        };
        
        IndividualClient individualClient = new IndividualClient
        {
            Client = client,
            FirstName = requestModel.FirstName,
            LastName = requestModel.LastName,
            Pesel = requestModel.Pesel,
        };
        
        await context.Clients.AddAsync(client);
        await context.IndividualClients.AddAsync(individualClient);
        
        await context.SaveChangesAsync();
    }

    public async Task<IndividualClient?> UpdateIndividualClient(UpdateIndividualClientRequestModel requestModel, int clientId)
    {
        IndividualClient? individualClient = await context.IndividualClients
            .Include(c => c.Client)
            .FirstOrDefaultAsync(c => c.IndividualClientId == clientId);
        
        if (individualClient == null)
            return null;
        
        individualClient.Client.Address = requestModel.Address;
        individualClient.Client.Email = requestModel.Email;
        individualClient.Client.PhoneNumber = requestModel.PhoneNumber;
        individualClient.FirstName = requestModel.FirstName;
        individualClient.LastName = requestModel.LastName;
        
        await context.SaveChangesAsync();
        return individualClient;
    }

    public async Task<IndividualClient?> DeleteIndividualClient(int clientId)
    {
        IndividualClient? individualClient = await context.IndividualClients
            .Include(c => c.Client)
            .FirstOrDefaultAsync(c => c.IndividualClientId == clientId);
        
        if (individualClient == null)
            return null;
        
        individualClient.DeletedAt = DateTime.Now;
        
        await context.SaveChangesAsync();
        return individualClient;
    }

    public async Task AddCorporateClient(int userId, CreateCorporateClientRequestModel requestModel)
    {
        Client client = new Client
        {
            ClientId = userId,
            Address = requestModel.Address,
            Email = requestModel.Email,
            PhoneNumber = requestModel.PhoneNumber,
        };
        
        CorporateClient corporateClient = new CorporateClient
        {
            Client = client,
            CompanyName = requestModel.CompanyName,
            KRS = requestModel.KRS
        };
        
        await context.Clients.AddAsync(client);
        await context.CorporateClients.AddAsync(corporateClient);
        
        await context.SaveChangesAsync();
    }

    public async Task<CorporateClient?> UpdateCorporateClient(UpdateCorporateClientRequestModel requestModel, int clientId)
    {
        CorporateClient? corporateClient = await context.CorporateClients
            .Include(c => c.Client)
            .FirstOrDefaultAsync(c => c.CorporateClientId == clientId);
        
        if (corporateClient == null)
            return null;
        
        corporateClient.Client.Address = requestModel.Address;
        corporateClient.Client.Email = requestModel.Email;
        corporateClient.Client.PhoneNumber = requestModel.PhoneNumber;
        
        await context.SaveChangesAsync();
        return corporateClient;
    }
}
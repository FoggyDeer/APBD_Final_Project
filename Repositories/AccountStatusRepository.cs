using APBD_Final_Project.DbContexts;
using APBD_Final_Project.DbContexts.Abstract;
using APBD_Final_Project.Entities;
using APBD_Final_Project.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace APBD_Final_Project.Repositories;

public class AccountStatusRepository(IApplicationContext context) : IAccountStatusRepository
{
    public async Task<bool> IsIndividualClientDeleted(int clientId)
    {
        IndividualClient? client = await context.IndividualClients
            .FirstOrDefaultAsync(c => c.IndividualClientId == clientId);
        
        return client?.DeletedAt != null;
    }
}
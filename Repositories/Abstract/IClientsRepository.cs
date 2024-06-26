using APBD_Final_Project.Entities;
using APBD_Final_Project.Models.ClientModels;

namespace APBD_Final_Project.Repositories.Abstract;

public interface IClientsRepository
{
    Task<bool> IsIndividualClientDeleted(int clientId);
    Task<bool> IsPeselValid(string pesel);
    Task<bool> IsKrsValid(string krs);
    Task AddIndividualClient(CreateIndividualClientRequestModel requestModel);
    Task<IndividualClient?> UpdateIndividualClient(UpdateIndividualClientRequestModel requestModel, int clientId);
    Task<IndividualClient?> DeleteIndividualClient(int clientId);
    Task AddCorporateClient(CreateCorporateClientRequestModel requestModel);
    Task<CorporateClient?> UpdateCorporateClient(UpdateCorporateClientRequestModel requestModel, int clientId);
}
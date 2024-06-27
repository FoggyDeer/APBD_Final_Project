using APBD_Final_Project.Entities;
using APBD_Final_Project.Models.ClientModels;

namespace APBD_Final_Project.Services.Abstract;

public interface IClientsService
{
    Task AddIndividualClient(int userId, CreateIndividualClientRequestModel requestModel);
    
    Task<IndividualClientResponseModel> UpdateIndividualClient(UpdateIndividualClientRequestModel requestModel, int clientId);
    
    Task DeleteIndividualClient(int clientId);
    
    Task AddCorporateClient(int userId, CreateCorporateClientRequestModel requestModel);
    
    Task<CorporateClientResponseModel> UpdateCorporateClient(UpdateCorporateClientRequestModel requestModel, int clientId);
}
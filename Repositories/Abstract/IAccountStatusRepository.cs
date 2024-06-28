namespace APBD_Final_Project.Services.Abstract;

public interface IAccountStatusRepository
{
    Task<bool> IsIndividualClientDeleted(int userId);
}
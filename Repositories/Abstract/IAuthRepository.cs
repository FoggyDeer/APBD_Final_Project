using APBD_Final_Project.Entities;

namespace APBD_Final_Project.Repositories.Abstract;

public interface IAuthRepository
{
    Task<User?> GetUserByLogin(string login);
    Task RegisterUser(User user);
}
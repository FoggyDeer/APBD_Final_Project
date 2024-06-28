using APBD_Final_Project.DbContexts;
using APBD_Final_Project.DbContexts.Abstract;
using APBD_Final_Project.Entities;
using APBD_Final_Project.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace APBD_Final_Project.Repositories;

public class AuthRepository(IApplicationContext context) : IAuthRepository
{
    public async Task<User?> GetUserByLogin(string login)
    {
        return await context.Users.FirstOrDefaultAsync(user => user.Login == login);
    }

    public async Task RegisterUser(User user)
    {
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
    }
}
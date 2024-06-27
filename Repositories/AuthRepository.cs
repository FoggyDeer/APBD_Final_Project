using APBD_Final_Project.DbContexts;
using APBD_Final_Project.Entities;
using APBD_Final_Project.Repositories.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace APBD_Final_Project.Repositories;

public class AuthRepository(ApplicationContext context) : IAuthRepository
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
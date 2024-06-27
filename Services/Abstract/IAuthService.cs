using JWT.Models;

namespace APBD_Final_Project.Services.Abstract;

public interface IAuthService
{
    Task<LoginResponseModel> Login(LoginRequestModel model);
    Task Register(RegisterRequestModel model);
    Task<RefreshTokenResponseModel> RefreshToken(RefreshTokenRequestModel model);
}
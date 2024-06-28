using APBD_Final_Project.Entities;
using APBD_Final_Project.Exceptions.AuthExceptions;
using APBD_Final_Project.Exceptions.ClientsException.Individual;
using APBD_Final_Project.Repositories.Abstract;
using APBD_Final_Project.Services.Abstract;
namespace APBD_Final_Project.Services;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JWT.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;


public class AuthService(IAuthRepository repository, IAccountStatusRepository accountStatusRepository, IConfiguration config) : IAuthService
{
    public async Task<LoginResponseModel> Login(LoginRequestModel model)
    {
        User? user = await repository.GetUserByLogin(model.Login);
        if (user == null)
            throw new UnauthorizedException();

        if (await accountStatusRepository.IsIndividualClientDeleted(user.UserId))
            throw new ClientDeletedException(ClientDeletedException.SelfMessage);
        
        var passwordHasher = new PasswordHasher<User>();
        var passwordIsCorrect = passwordHasher.VerifyHashedPassword(user, user.Password, model.Password) == PasswordVerificationResult.Success;
        if(!passwordIsCorrect)
            throw new UnauthorizedException();
        
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new("username", user.Login),
            new(ClaimTypes.Role, user.Role)
        };
            
        var stringToken = GenerateAccessToken(claims);
        var stringRefToken = GenerateRefreshToken(claims);
        
        return new LoginResponseModel
        {
            Token = stringToken,
            RefreshToken = stringRefToken
        };
    }

    public async Task Register(RegisterRequestModel model)
    {
        var user = await repository.GetUserByLogin(model.Login);
        if (user != null)
            throw new UserExistException(model.Login);
        
        user = new User
        {
            Login = model.Login
        };
        
        var passwordHasher = new PasswordHasher<User>();
        var hashedPassword = passwordHasher.HashPassword(user, model.Password);
        user.Password = hashedPassword;
        
        await repository.RegisterUser(user);
    }
    
    public async Task<RefreshTokenResponseModel> RefreshToken(RefreshTokenRequestModel model)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            tokenHandler.ValidateToken(model.RefreshToken, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = config["JWT:RefIssuer"],
                ValidAudience = config["JWT:RefAudience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:RefKey"]!))
            }, out _);

            var refreshToken = tokenHandler.ReadJwtToken(model.RefreshToken);
            
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, refreshToken.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Sub).Value),
                new("username", refreshToken.Claims.First(claim => claim.Type == "username").Value),
                new(ClaimTypes.Role, refreshToken.Claims.First(claim => claim.Type == ClaimTypes.Role).Value)
            };
            
            var stringToken = GenerateAccessToken(claims);
            var stringRefToken = GenerateRefreshToken(claims);
        
            return new RefreshTokenResponseModel
            {
                Token = stringToken,
                RefreshToken = stringRefToken
            };
        }
        catch
        {
            throw new WrongTokenException();
        }
    }

    private string GenerateAccessToken(IEnumerable<Claim> claims)
    {
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescription = new SecurityTokenDescriptor
        {
            Issuer = config["JWT:Issuer"],
            Audience = config["JWT:Audience"],
            Expires = DateTime.UtcNow.AddMinutes(15),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"]!)),
                SecurityAlgorithms.HmacSha256
            ),
            Subject = new ClaimsIdentity(claims)
        };
        var token = tokenHandler.CreateToken(tokenDescription);
        return tokenHandler.WriteToken(token);
    }

    private string GenerateRefreshToken(IEnumerable<Claim> claims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var refTokenDescription = new SecurityTokenDescriptor
        {
            Issuer = config["JWT:RefIssuer"],
            Audience = config["JWT:RefAudience"],
            Expires = DateTime.UtcNow.AddDays(3),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:RefKey"]!)),
                SecurityAlgorithms.HmacSha256
            ),
            Subject = new ClaimsIdentity(claims)
        };
        var refToken = tokenHandler.CreateToken(refTokenDescription);
        return tokenHandler.WriteToken(refToken);
    }
}
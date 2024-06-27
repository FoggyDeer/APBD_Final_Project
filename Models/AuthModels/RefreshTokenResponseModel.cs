namespace JWT.Models;

public class RefreshTokenResponseModel
{
    public string Token { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
}
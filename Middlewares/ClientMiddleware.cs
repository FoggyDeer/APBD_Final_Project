using System.IdentityModel.Tokens.Jwt;
using System.Text.RegularExpressions;
using APBD_Final_Project.Exceptions.ClientsException;
using APBD_Final_Project.Exceptions.ClientsException.Individual;
using APBD_Final_Project.Services.Abstract;
using Microsoft.IdentityModel.Tokens;

namespace APBD_Final_Project.Middlewares;

public class ClientMiddleware
{
    private readonly RequestDelegate _next;
    
    public ClientMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IAccountStatusRepository accountStatusRepository)
    {
        try
        {
            var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();

            if (!token.IsNullOrEmpty() && handler.ReadToken(token) is JwtSecurityToken jwtToken)
            {
                var sub = jwtToken.Claims.First(claim => claim.Type == "sub")?.Value;
                if(sub == null || Regex.IsMatch(sub, @"\D"))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.ContentType = "application/text";
                    await context.Response.WriteAsync("Unauthorized");
                    return;
                }
                
                int userId = int.Parse(sub);
                if (await accountStatusRepository.IsIndividualClientDeleted(userId))
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    context.Response.ContentType = "application/text";
                    await context.Response.WriteAsync(ClientDeletedException.SelfMessage);
                    return;
                }
            }
            
            await _next(context);
        }
        catch (Exception e) when (e is ClientDeletedException or UserAlreadyClientException)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/text";
            await context.Response.WriteAsync(e.Message);
        }
        catch (ClientNotFoundException e)
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            context.Response.ContentType = "application/text";
            await context.Response.WriteAsync(e.Message);
        }
    }
}
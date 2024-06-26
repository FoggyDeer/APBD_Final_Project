using APBD_Final_Project.Exceptions.ClientsException.Individual;
namespace APBD_Final_Project.Middlewares;

public class IndividualClientMiddleware
{
    private readonly RequestDelegate _next;

    public IndividualClientMiddleware(RequestDelegate next, ILogger<IndividualClientMiddleware> logger)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ClientDeletedException e)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/text";
            await context.Response.WriteAsync(e.Message);
        }
    }
}
using System.ComponentModel;
using APBD_Final_Project.Exceptions.RevenueExceptions;
using APBD_Final_Project.Exceptions.SoftwareExceptions;
using APBD_Final_Project.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Final_Project.Controllers;

[ApiController]
[Route("/api/revenue")]
[Authorize]
public class RevenueController(IRevenueService revenueService) : ControllerBase
{
    [HttpGet]
    [Route("current/")]
    public async Task<IActionResult> GetCurrentRevenue([FromQuery] int? productId = null, [FromQuery] [DefaultValue("PLN")] string currency = "PLN")
    {
        try
        {
            return Ok(await revenueService.GetCurrentRevenue(currency, productId));
        }
        catch (SoftwareNotFoundException e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet]
    [Route("forecast/")]
    public async Task<IActionResult> GetForecastRevenue([FromQuery] int? productId = null, [FromQuery] [DefaultValue("PLN")] string currency = "PLN")
    {
        try
        {
            return Ok(await revenueService.GetForecastRevenue(currency, productId));
        }
        catch (Exception e) when (e is SoftwareNotFoundException or CurrencyConvertException or InvalidCurrencyException)
        {
            return BadRequest(e.Message);
        }
    }
}
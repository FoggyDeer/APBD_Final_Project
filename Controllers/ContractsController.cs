using System.Security.Claims;
using System.Text.RegularExpressions;
using APBD_Final_Project.Exceptions.ContractExceptions;
using APBD_Final_Project.Exceptions.SoftwareExceptions;
using APBD_Final_Project.Models.ContractModels;
using APBD_Final_Project.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Final_Project.Controllers;

[ApiController]
[Route("api/contracts")]
[Authorize]
public class ContractsController(IContractsService contractsService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateContract(CreateContractRequestModel requestModel)
    {
        var sub = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if(sub == null || Regex.IsMatch(sub, @"\D"))
        {
            return Unauthorized();
        }
        
        try
        {
            int userId = int.Parse(sub);
            await contractsService.CreateContract(userId, requestModel);
            return Created();
        }
        catch (Exception e) when (e is ActiveContractException or ContractStartDateException or InvalidPeriodException or SoftwareNotFoundException)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPost]
    [Route("api/contracts/{contractId:int}/invoices")]
    public async Task<IActionResult> CreateInvoice(int contractId, CreateInvoiceRequestModel requestModel)
    {
        var sub = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if(sub == null || Regex.IsMatch(sub, @"\D"))
        {
            return Unauthorized();
        }
        
        try
        {
            int userId = int.Parse(sub);
            await contractsService.CreateInvoice(contractId, userId, requestModel);
            return Created();
        }
        catch (Exception e) when (e is ContractNotFoundException or ContractAlreadyPaidException or ContractExpiredException)
        {
            return BadRequest(e.Message);
        }
    }
}
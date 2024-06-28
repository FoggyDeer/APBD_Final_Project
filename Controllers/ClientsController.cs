using System.Security.Claims;
using System.Text.RegularExpressions;
using APBD_Final_Project.Exceptions.ClientsException.Corporate;
using APBD_Final_Project.Exceptions.ClientsException.Individual;
using APBD_Final_Project.Models.ClientModels;
using APBD_Final_Project.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Final_Project.Controllers;

[ApiController]
[Route("api/clients")]
[Authorize]
public class ClientsController(IClientsService clientsService) : ControllerBase
{
    
    [HttpPost]
    [Route("Individual")]
    public async Task<IActionResult> AddIndividualClient(CreateIndividualClientRequestModel requestModel)
    {
        var sub = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if(sub == null || Regex.IsMatch(sub, @"\D"))
        {
            return Unauthorized();
        }
        
        try
        {
            int userId = int.Parse(sub);
            await clientsService.AddIndividualClient(userId, requestModel);
            return Created();
        }
        catch (Exception e) when (e is PeselIsNotValidException)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPut]
    [Route("Individual/{clientId:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateIndividualClient(UpdateIndividualClientRequestModel requestModel, int clientId)
    {
        try
        {
            return Ok(await clientsService.UpdateIndividualClient(requestModel, clientId));
        }
        catch (IndividualClientNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
    
    [HttpDelete]
    [Route("Individual/{clientId:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteIndividualClient(int clientId)
    {
        try
        {
            await clientsService.DeleteIndividualClient(clientId);
            return Ok();
        }
        catch (IndividualClientNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
    
    [HttpPost]
    [Route("Corporate")]
    public async Task<IActionResult> AddCorporateClient(CreateCorporateClientRequestModel requestModel)
    {
        var sub = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if(sub == null || Regex.IsMatch(sub, @"\D"))
        {
            return Unauthorized();
        }
        
        try
        {
            int userId = int.Parse(sub);
            await clientsService.AddCorporateClient(userId, requestModel);
            return Created();
        }
        catch (Exception e) when (e is KrsIsNotValidException)
        {
            return BadRequest(e.Message);
        }
    }
    
    
    [HttpPut]
    [Route("Corporate/{clientId:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateCorporateClient(UpdateCorporateClientRequestModel requestModel, int clientId)
    {
        try
        {
            return Ok(await clientsService.UpdateCorporateClient(requestModel, clientId));
        }
        catch (CorporateClientNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
    
}
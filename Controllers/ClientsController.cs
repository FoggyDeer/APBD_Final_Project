using APBD_Final_Project.Exceptions.ClientsException.Corporate;
using APBD_Final_Project.Exceptions.ClientsException.Individual;
using APBD_Final_Project.Models.ClientModels;
using APBD_Final_Project.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Final_Project.Controllers;

[ApiController]
[Route("api/clients")]
public class ClientsController(IClientsService clientsService) : ControllerBase
{
    
    [HttpPost]
    [Route("Individual")]
    public async Task<IActionResult> AddIndividualClient(CreateIndividualClientRequestModel requestModel)
    {
        try
        {
            await clientsService.AddIndividualClient(requestModel);
            return Created();
        }
        catch (PeselIsNotValidException e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPut]
    [Route("Individual/{clientId:int}")]
    public async Task<IActionResult> UpdateIndividualClient(UpdateIndividualClientRequestModel requestModel, int clientId)
    {
        try
        {
            return Ok(await clientsService.UpdateIndividualClient(requestModel, clientId));
        }
        catch (Exceptions.ClientsException.Individual.NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
    
    [HttpDelete]
    [Route("Individual/{clientId:int}")]
    public async Task<IActionResult> DeleteIndividualClient(int clientId)
    {
        try
        {
            await clientsService.DeleteIndividualClient(clientId);
            return Ok();
        }
        catch (Exceptions.ClientsException.Individual.NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
    
    [HttpPost]
    [Route("Corporate")]
    public async Task<IActionResult> AddCorporateClient(CreateCorporateClientRequestModel requestModel)
    {
        try
        {
            await clientsService.AddCorporateClient(requestModel);
            return Created();
        }
        catch (KrsIsNotValidException e)
        {
            return BadRequest(e.Message);
        }
    }
    
    
    [HttpPut]
    [Route("Corporate/{clientId:int}")]
    public async Task<IActionResult> UpdateCorporateClient(UpdateCorporateClientRequestModel requestModel, int clientId)
    {
        try
        {
            return Ok(await clientsService.UpdateCorporateClient(requestModel, clientId));
        }
        catch (Exceptions.ClientsException.Corporate.NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
    
}
using Microsoft.AspNetCore.Mvc;
using Shopee.EmployeeCRM.Api.Services;
using Shopee.EmployeeCRM.Shared.Dtos;

namespace Shopee.EmployeeCRM.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController(IClientService clientService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<ClientDto>>> GetAll()
    {
        var clients = await clientService.GetAllAsync();
        return Ok(clients);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ClientDto>> GetById(int id)
    {
        var client = await clientService.GetByIdAsync(id);
        return client is null ? NotFound() : Ok(client);
    }

    [HttpPost]
    public async Task<ActionResult<ClientDto>> Create(ClientDto clientDto)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var createdClient = await clientService.AddAsync(clientDto);
        return CreatedAtAction(nameof(GetById), new { id = createdClient.Id }, createdClient);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, ClientDto clientDto)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var updated = await clientService.UpdateAsync(id, clientDto);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await clientService.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}

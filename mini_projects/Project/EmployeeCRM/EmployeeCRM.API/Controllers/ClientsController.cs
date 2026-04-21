using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EmployeeCRM.Core.DTOs;
using EmployeeCRM.Core.Interfaces;

namespace EmployeeCRM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClients()
        {
            try
            {
                var clients = await _clientService.GetAllClientsAsync();
                return Ok(clients);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving clients", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientById(int id)
        {
            try
            {
                var client = await _clientService.GetClientByIdAsync(id);
                return Ok(client);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving client", error = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> CreateClient([FromBody] ClientDTO clientDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _clientService.CreateClientAsync(clientDTO);
                return CreatedAtAction(nameof(GetClientById), new { id = result.Id }, result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error creating client", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> UpdateClient(int id, [FromBody] ClientDTO clientDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _clientService.UpdateClientAsync(id, clientDTO);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error updating client", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            try
            {
                var result = await _clientService.DeleteClientAsync(id);
                if (!result)
                    return NotFound(new { message = "Client not found" });

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error deleting client", error = ex.Message });
            }
        }

        [HttpGet("search/{searchTerm}")]
        public async Task<IActionResult> SearchClients(string searchTerm)
        {
            try
            {
                var result = await _clientService.SearchClientsAsync(searchTerm);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error searching clients", error = ex.Message });
            }
        }

        [HttpGet("employee/{employeeId}")]
        public async Task<IActionResult> GetClientsByEmployee(int employeeId)
        {
            try
            {
                var result = await _clientService.GetClientsByEmployeeAsync(employeeId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving clients", error = ex.Message });
            }
        }
    }
}
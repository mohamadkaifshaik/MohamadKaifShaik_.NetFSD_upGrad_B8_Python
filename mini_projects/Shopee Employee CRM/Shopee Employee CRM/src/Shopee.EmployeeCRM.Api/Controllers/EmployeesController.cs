using Microsoft.AspNetCore.Mvc;
using Shopee.EmployeeCRM.Api.Services;
using Shopee.EmployeeCRM.Shared.Dtos;

namespace Shopee.EmployeeCRM.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController(IEmployeeService employeeService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<EmployeeDto>>> GetAll([FromQuery] string? searchText)
    {
        var employees = await employeeService.GetAllAsync(searchText);
        return Ok(employees);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<EmployeeDto>> GetById(int id)
    {
        var employee = await employeeService.GetByIdAsync(id);
        return employee is null ? NotFound() : Ok(employee);
    }

    [HttpPost]
    public async Task<ActionResult<EmployeeDto>> Create(EmployeeDto employeeDto)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var createdEmployee = await employeeService.AddAsync(employeeDto);
        return CreatedAtAction(nameof(GetById), new { id = createdEmployee.Id }, createdEmployee);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, EmployeeDto employeeDto)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var updated = await employeeService.UpdateAsync(id, employeeDto);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await employeeService.DeleteAsync(id);
        return deleted ? NoContent() : BadRequest("Employee cannot be deleted while clients or tasks are still assigned.");
    }
}

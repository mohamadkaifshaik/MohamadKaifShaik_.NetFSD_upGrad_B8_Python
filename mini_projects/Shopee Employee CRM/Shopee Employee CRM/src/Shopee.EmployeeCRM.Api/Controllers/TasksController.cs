using Microsoft.AspNetCore.Mvc;
using Shopee.EmployeeCRM.Api.Services;
using Shopee.EmployeeCRM.Shared.Dtos;

namespace Shopee.EmployeeCRM.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController(IWorkTaskService taskService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<WorkTaskDto>>> GetAll()
    {
        var tasks = await taskService.GetAllAsync();
        return Ok(tasks);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<WorkTaskDto>> GetById(int id)
    {
        var task = await taskService.GetByIdAsync(id);
        return task is null ? NotFound() : Ok(task);
    }

    [HttpPost]
    public async Task<ActionResult<WorkTaskDto>> Create(WorkTaskDto taskDto)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var createdTask = await taskService.AddAsync(taskDto);
        return CreatedAtAction(nameof(GetById), new { id = createdTask.Id }, createdTask);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, WorkTaskDto taskDto)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var updated = await taskService.UpdateAsync(id, taskDto);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await taskService.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}

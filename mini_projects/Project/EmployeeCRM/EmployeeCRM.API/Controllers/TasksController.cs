using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EmployeeCRM.Core.DTOs;
using EmployeeCRM.Core.Enums;
using EmployeeCRM.Core.Interfaces;
using TaskStatusEnum = EmployeeCRM.Core.Enums.TaskStatus;

namespace EmployeeCRM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            try
            {
                var tasks = await _taskService.GetAllTasksAsync();
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving tasks", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            try
            {
                var task = await _taskService.GetTaskByIdAsync(id);
                return Ok(task);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving task", error = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager,Employee")]
        public async Task<IActionResult> CreateTask([FromBody] TaskDTO taskDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _taskService.CreateTaskAsync(taskDTO);
                //return CreatedAtAction(nameof(GetTaskById), new { id = result.Id }, result);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error creating task", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager,Employee")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] TaskDTO taskDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _taskService.UpdateTaskAsync(id, taskDTO);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error updating task", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            try
            {
                var result = await _taskService.DeleteTaskAsync(id);
                if (!result)
                    return NotFound(new { message = "Task not found" });

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error deleting task", error = ex.Message });
            }
        }

        [HttpGet("employee/{employeeId}")]
        public async Task<IActionResult> GetTasksByEmployee(int employeeId)
        {
            try
            {
                var result = await _taskService.GetTasksByEmployeeAsync(employeeId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving tasks", error = ex.Message });
            }
        }

        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetTasksByStatus(TaskStatusEnum status)
        {
            try
            {
                //var taskStatus = (TaskStatusEnum)status;
                var result = await _taskService.GetTasksByStatusAsync(status);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving tasks", error = ex.Message });
            }
        }

        [HttpGet("overdue/list")]
        public async Task<IActionResult> GetOverdueTasks()
        {
            try
            {
                var result = await _taskService.GetOverdueTasksAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving overdue tasks", error = ex.Message });
            }
        }
    }
}
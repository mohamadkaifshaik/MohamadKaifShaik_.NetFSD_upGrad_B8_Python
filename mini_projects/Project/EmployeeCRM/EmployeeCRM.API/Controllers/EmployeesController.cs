using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EmployeeCRM.Core.DTOs;
using EmployeeCRM.Core.Interfaces;

namespace EmployeeCRM.API.Controllers
{
    /// <summary>
    /// API Controller for Employee Management
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Requires authentication
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary>
        /// Get all employees
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<EmployeeDTO>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                var employees = await _employeeService.GetAllEmployeesAsync();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving employees", error = ex.Message });
            }
        }

        /// <summary>
        /// Get employee by ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EmployeeDTO), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            try
            {
                var employee = await _employeeService.GetEmployeeByIdAsync(id);
                return Ok(employee);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving employee", error = ex.Message });
            }
        }

        /// <summary>
        /// Create new employee
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin,Manager")] // Only Admin or Manager can create
        [ProducesResponseType(typeof(EmployeeDTO), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeDTO employeeDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _employeeService.CreateEmployeeAsync(employeeDTO);
                return CreatedAtAction(nameof(GetEmployeeById), new { id = result.Id }, result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error creating employee", error = ex.Message });
            }
        }

        /// <summary>
        /// Update employee
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        [ProducesResponseType(typeof(EmployeeDTO), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeDTO employeeDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _employeeService.UpdateEmployeeAsync(id, employeeDTO);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error updating employee", error = ex.Message });
            }
        }

        /// <summary>
        /// Delete employee
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                var result = await _employeeService.DeleteEmployeeAsync(id);

                if (!result)
                    return NotFound(new { message = "Employee not found" });

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error deleting employee", error = ex.Message });
            }
        }

        /// <summary>
        /// Search employees by name or email
        /// </summary>
        [HttpGet("search/{searchTerm}")]
        [ProducesResponseType(typeof(IEnumerable<EmployeeDTO>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> SearchEmployees(string searchTerm)
        {
            try
            {
                var result = await _employeeService.SearchEmployeesAsync(searchTerm);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error searching employees", error = ex.Message });
            }
        }

        /// <summary>
        /// Get employees by status
        /// </summary>
        [HttpGet("status/{statusId}")]
        [ProducesResponseType(typeof(IEnumerable<EmployeeDTO>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetEmployeesByStatus(int statusId)
        {
            try
            {
                var result = await _employeeService.GetEmployeesByStatusAsync(statusId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving employees by status", error = ex.Message });
            }
        }
    }
}
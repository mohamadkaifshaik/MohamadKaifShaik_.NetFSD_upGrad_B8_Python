using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeCRM.Core.DTOs;
using EmployeeCRM.MVC.Services;

namespace EmployeeCRM.MVC.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeApiClient _employeeApiClient;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeApiClient employeeApiClient, ILogger<EmployeeController> logger)
        {
            _employeeApiClient = employeeApiClient;
            _logger = logger;
        }

        // List all employees
        public async Task<IActionResult> Index(string searchTerm = "")
        {
            try
            {
                IEnumerable<EmployeeDTO> employees;

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    var token = HttpContext.Session.GetString("JwtToken");
                    employees = await _employeeApiClient.SearchAsync(searchTerm, token);
                    ViewBag.SearchTerm = searchTerm;
                }
                else
                {
                    var token = HttpContext.Session.GetString("JwtToken");
                    employees = await _employeeApiClient.GetAllAsync(token);
                }

                return View(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error loading employees: {ex.Message}");
                TempData["Error"] = "Error loading employees";
                return View(new List<EmployeeDTO>());
            }
        }

        // View employee details
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var token = HttpContext.Session.GetString("JwtToken");
                var employee = await _employeeApiClient.GetByIdAsync(id, token);

                if (employee == null)
                {
                    TempData["Error"] = "Employee not found";
                    return RedirectToAction("Index");
                }

                return View(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error loading employee details: {ex.Message}");
                TempData["Error"] = "Error loading employee details";
                return RedirectToAction("Index");
            }
        }

        // Display Create Employee Form
        [Authorize(Roles = "Admin,Manager,Employee")]
        
        public IActionResult Create()
        {
            return View(new EmployeeDTO());
        }

        // Handle Create Employee Form Submission
        [HttpPost]
        [ValidateAntiForgeryToken]

        [Authorize(Roles = "Admin,Manager,Employee")]
        public async Task<IActionResult> Create(EmployeeDTO employeeDTO)
        {
            if (!ModelState.IsValid)
                return View(employeeDTO);

            try
            {
                var token = HttpContext.Session.GetString("JwtToken");
                await _employeeApiClient.CreateAsync(employeeDTO, token);

                TempData["Success"] = "Employee created successfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating employee: {ex.Message}");
                ModelState.AddModelError("", "Error creating employee: " + ex.Message);
                return View(employeeDTO);
            }
        }

        // Display Edit Employee Form
        [Authorize(Roles = "Admin,Manager,Employee")]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var token = HttpContext.Session.GetString("JwtToken");
                var employee = await _employeeApiClient.GetByIdAsync(id, token);

                if (employee == null)
                {
                    TempData["Error"] = "Employee not found";
                    return RedirectToAction("Index");
                }

                return View(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error loading employee for edit: {ex.Message}");
                TempData["Error"] = "Error loading employee";
                return RedirectToAction("Index");
            }
        }

        // Handle Edit Employee Form Submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager,Employee")]
        public async Task<IActionResult> Edit(int id, EmployeeDTO employeeDTO)
        {
            if (!ModelState.IsValid)
                return View(employeeDTO);

            try
            {
                var token = HttpContext.Session.GetString("JwtToken");
                await _employeeApiClient.UpdateAsync(id, employeeDTO, token);

                TempData["Success"] = "Employee updated successfully";
                return RedirectToAction("Details", new { id });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating employee: {ex.Message}");
                ModelState.AddModelError("", "Error updating employee: " + ex.Message);
                return View(employeeDTO);
            }
        }

        // Delete Employee
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var token = HttpContext.Session.GetString("JwtToken");
                await _employeeApiClient.DeleteAsync(id, token);

                TempData["Success"] = "Employee deleted successfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting employee: {ex.Message}");
                TempData["Error"] = "Error deleting employee: " + ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}
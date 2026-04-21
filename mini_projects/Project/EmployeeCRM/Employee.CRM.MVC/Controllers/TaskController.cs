using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeCRM.Core.DTOs;
using EmployeeCRM.MVC.Services;

namespace EmployeeCRM.MVC.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private readonly ITaskApiClient _taskApiClient;
        private readonly ILogger<TaskController> _logger;

        public TaskController(ITaskApiClient taskApiClient, ILogger<TaskController> logger)
        {
            _taskApiClient = taskApiClient;
            _logger = logger;
        }

        // List all tasks
        public async Task<IActionResult> Index()
        {
            try
            {
                var token = HttpContext.Session.GetString("JwtToken");
                var tasks = await _taskApiClient.GetAllAsync(token);

                return View(tasks);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error loading tasks: {ex.Message}");
                TempData["Error"] = "Error loading tasks";
                return View(new List<TaskDTO>());
            }
        }

        // View task details
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var token = HttpContext.Session.GetString("JwtToken");
                var task = await _taskApiClient.GetByIdAsync(id, token);

                if (task == null)
                {
                    TempData["Error"] = "Task not found";
                    return RedirectToAction("Index");
                }

                return View(task);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error loading task details: {ex.Message}");
                TempData["Error"] = "Error loading task details";
                return RedirectToAction("Index");
            }
        }

        // Display Create Task Form
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult Create()
        {
            return View(new TaskDTO());
        }

        // Handle Create Task Form Submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager,Employee")]
        public async Task<IActionResult> Create(TaskDTO taskDTO)
        {
            //var token = User.Claims.FirstOrDefault(c => c.Type == "Token")?.Value;
            //return Content(token ?? "Step 2: Token null");
            if (!ModelState.IsValid)
                //return Content("STEP 1: Hit");
                return View(taskDTO);

            try
            {
                var token = User.Claims.FirstOrDefault(c => c.Type == "Token")?.Value;
                if (string.IsNullOrEmpty(token))
                {
                    return Content("TOKEN IS NULL");
                }
                await _taskApiClient.CreateAsync(taskDTO, token);

                TempData["Success"] = "Task created successfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating task: {ex.Message}");
                ModelState.AddModelError("", "Error creating task: " + ex.Message);
                return View(taskDTO);
            }
        }

        // Display Edit Task Form
        [Authorize(Roles = "Admin,Manager,Employee")]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var token = HttpContext.Session.GetString("JwtToken");
                var task = await _taskApiClient.GetByIdAsync(id, token);

                if (task == null)
                {
                    TempData["Error"] = "Task not found";
                    return RedirectToAction("Index");
                }

                return View(task);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error loading task for edit: {ex.Message}");
                TempData["Error"] = "Error loading task";
                return RedirectToAction("Index");
            }
        }

        // Handle Edit Task Form Submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager,Employee")]
        public async Task<IActionResult> Edit(int id, TaskDTO taskDTO)
        {
            if (!ModelState.IsValid)
                return View(taskDTO);

            try
            {
                var token = HttpContext.Session.GetString("JwtToken");
                await _taskApiClient.UpdateAsync(id, taskDTO, token);

                TempData["Success"] = "Task updated successfully";
                return RedirectToAction("Details", new { id });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating task: {ex.Message}");
                ModelState.AddModelError("", "Error updating task: " + ex.Message);
                return View(taskDTO);
            }
        }

        // Delete Task
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var token = HttpContext.Session.GetString("JwtToken");
                await _taskApiClient.DeleteAsync(id, token);

                TempData["Success"] = "Task deleted successfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting task: {ex.Message}");
                TempData["Error"] = "Error deleting task: " + ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shopee.EmployeeCRM.Mvc.Services;
using Shopee.EmployeeCRM.Mvc.ViewModels;
using Shopee.EmployeeCRM.Shared.Dtos;
using Shopee.EmployeeCRM.Shared.Enums;

namespace Shopee.EmployeeCRM.Mvc.Controllers;

public class TasksController(IApiService apiService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var tasks = await apiService.GetTasksAsync();
        return View(tasks);
    }

    public async Task<IActionResult> Create() =>
        View("Form", await BuildTaskFormViewModelAsync(new WorkTaskDto(), "Add Task"));

    [HttpPost]
    public async Task<IActionResult> Create(WorkTaskDto task)
    {
        if (!ModelState.IsValid)
        {
            return View("Form", await BuildTaskFormViewModelAsync(task, "Add Task"));
        }

        var created = await apiService.CreateTaskAsync(task);
        if (!created)
        {
            ModelState.AddModelError(string.Empty, "Unable to save task.");
            return View("Form", await BuildTaskFormViewModelAsync(task, "Add Task"));
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var task = await apiService.GetTaskByIdAsync(id);
        if (task is null)
        {
            return NotFound();
        }

        return View("Form", await BuildTaskFormViewModelAsync(task, "Edit Task"));
    }

    [HttpPost]
    public async Task<IActionResult> Edit(WorkTaskDto task)
    {
        if (!ModelState.IsValid)
        {
            return View("Form", await BuildTaskFormViewModelAsync(task, "Edit Task"));
        }

        var updated = await apiService.UpdateTaskAsync(task);
        if (!updated)
        {
            ModelState.AddModelError(string.Empty, "Unable to update task.");
            return View("Form", await BuildTaskFormViewModelAsync(task, "Edit Task"));
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        await apiService.DeleteTaskAsync(id);
        return RedirectToAction(nameof(Index));
    }

    private async Task<TaskFormViewModel> BuildTaskFormViewModelAsync(WorkTaskDto task, string pageTitle)
    {
        var employees = await apiService.GetEmployeesAsync();
        return new TaskFormViewModel
        {
            Task = task,
            PageTitle = pageTitle,
            Employees = employees
                .Select(employee => new SelectListItem(employee.FullName, employee.Id.ToString()))
                .ToList(),
            StatusOptions = Enum.GetValues<WorkStatus>()
                .Select(status => new SelectListItem(status.ToString(), status.ToString()))
                .ToList()
        };
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shopee.EmployeeCRM.Mvc.Services;
using Shopee.EmployeeCRM.Mvc.ViewModels;
using Shopee.EmployeeCRM.Shared.Dtos;
using Shopee.EmployeeCRM.Shared.Enums;

namespace Shopee.EmployeeCRM.Mvc.Controllers;

public class EmployeesController(IApiService apiService) : Controller
{
    public async Task<IActionResult> Index(string? searchText)
    {
        var employees = await apiService.GetEmployeesAsync(searchText);
        ViewBag.SearchText = searchText;
        return View(employees);
    }

    public IActionResult Create() =>
        View("Form", BuildEmployeeFormViewModel(new EmployeeDto(), "Add Employee"));

    [HttpPost]
    public async Task<IActionResult> Create(EmployeeDto employee)
    {
        if (!ModelState.IsValid)
        {
            return View("Form", BuildEmployeeFormViewModel(employee, "Add Employee"));
        }

        var created = await apiService.CreateEmployeeAsync(employee);
        if (!created)
        {
            ModelState.AddModelError(string.Empty, "Unable to save employee.");
            return View("Form", BuildEmployeeFormViewModel(employee, "Add Employee"));
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var employee = await apiService.GetEmployeeByIdAsync(id);
        if (employee is null)
        {
            return NotFound();
        }

        return View("Form", BuildEmployeeFormViewModel(employee, "Edit Employee"));
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EmployeeDto employee)
    {
        if (!ModelState.IsValid)
        {
            return View("Form", BuildEmployeeFormViewModel(employee, "Edit Employee"));
        }

        var updated = await apiService.UpdateEmployeeAsync(employee);
        if (!updated)
        {
            ModelState.AddModelError(string.Empty, "Unable to update employee.");
            return View("Form", BuildEmployeeFormViewModel(employee, "Edit Employee"));
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        await apiService.DeleteEmployeeAsync(id);
        return RedirectToAction(nameof(Index));
    }

    private static EmployeeFormViewModel BuildEmployeeFormViewModel(EmployeeDto employee, string pageTitle) =>
        new()
        {
            Employee = employee,
            PageTitle = pageTitle,
            RoleOptions = Enum.GetValues<EmployeeRole>()
                .Select(role => new SelectListItem(role.ToString(), role.ToString()))
                .ToList()
        };
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shopee.EmployeeCRM.Mvc.Services;
using Shopee.EmployeeCRM.Mvc.ViewModels;
using Shopee.EmployeeCRM.Shared.Dtos;

namespace Shopee.EmployeeCRM.Mvc.Controllers;

public class ClientsController(IApiService apiService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var clients = await apiService.GetClientsAsync();
        return View(clients);
    }

    public async Task<IActionResult> Create() =>
        View("Form", await BuildClientFormViewModelAsync(new ClientDto(), "Add Client"));

    [HttpPost]
    public async Task<IActionResult> Create(ClientDto client)
    {
        if (!ModelState.IsValid)
        {
            return View("Form", await BuildClientFormViewModelAsync(client, "Add Client"));
        }

        var created = await apiService.CreateClientAsync(client);
        if (!created)
        {
            ModelState.AddModelError(string.Empty, "Unable to save client.");
            return View("Form", await BuildClientFormViewModelAsync(client, "Add Client"));
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var client = await apiService.GetClientByIdAsync(id);
        if (client is null)
        {
            return NotFound();
        }

        return View("Form", await BuildClientFormViewModelAsync(client, "Edit Client"));
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ClientDto client)
    {
        if (!ModelState.IsValid)
        {
            return View("Form", await BuildClientFormViewModelAsync(client, "Edit Client"));
        }

        var updated = await apiService.UpdateClientAsync(client);
        if (!updated)
        {
            ModelState.AddModelError(string.Empty, "Unable to update client.");
            return View("Form", await BuildClientFormViewModelAsync(client, "Edit Client"));
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        await apiService.DeleteClientAsync(id);
        return RedirectToAction(nameof(Index));
    }

    private async Task<ClientFormViewModel> BuildClientFormViewModelAsync(ClientDto client, string pageTitle)
    {
        var employees = await apiService.GetEmployeesAsync();
        return new ClientFormViewModel
        {
            Client = client,
            PageTitle = pageTitle,
            Employees = employees
                .Select(employee => new SelectListItem(employee.FullName, employee.Id.ToString()))
                .ToList()
        };
    }
}

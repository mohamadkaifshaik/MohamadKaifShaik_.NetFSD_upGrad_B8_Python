using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Shopee.EmployeeCRM.Mvc.Models;
using Shopee.EmployeeCRM.Mvc.Services;
using Shopee.EmployeeCRM.Mvc.ViewModels;

namespace Shopee.EmployeeCRM.Mvc.Controllers;

public class HomeController(IApiService apiService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var summary = await apiService.GetDashboardAsync() ?? new();
        return View(new DashboardViewModel { Summary = summary });
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
